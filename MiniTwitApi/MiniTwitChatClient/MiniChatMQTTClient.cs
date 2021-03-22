using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using MiniTwitChatClient;
using MiniTwitChatClient.Abstractions;
using MiniTwitChatClient.Models;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace MiniTwitChatClient
{
    public class MiniChatMQTTClient : IMiniChatClient
    {
        public Action<ChatMessage> ReceivedMessage
        {
            get => _receivedMessage;
            set => _receivedMessage = value;
        }
        private Action<ChatMessage> _receivedMessage;
        private readonly ChatConfiguration _configuration = new ("pythonkindergarten.tech", 15676,  "minitwit", "minitwit");
        private List<string> _subscribedThreads = new List<string>();
        private IManagedMqttClient _mqttClient;

        public MiniChatMQTTClient(ChatConfiguration configuration = null)
        {
            if (configuration != null)
                _configuration = configuration;
        }
        
        public async Task PublishMessageAsync(ChatMessage message)
        {
            Console.WriteLine($"Sending message {JsonSerializer.Serialize(message)}");
            var result = await _mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic($"messages.{message.ThreadId}")
                .WithPayload(JsonSerializer.Serialize(message))
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)1)
                .WithRetainFlag(false)
                .Build());
            Console.WriteLine($"Sent message with topic: messages.{message.ThreadId} {result.ReasonCode}");
        }

        public async Task SubscribeAsync(List<string> chatThreads)
        {
            _subscribedThreads = chatThreads;

            foreach (var thread in _subscribedThreads)
                await _mqttClient.SubscribeAsync(new TopicFilterBuilder()
                    .WithTopic($"messages.{thread}")
                    .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)1)
                    .Build());
        }
        
        public async Task InitializeAsync()
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithCredentials(_configuration.BrokerUser, _configuration.BrokerPassword)
                .WithWebSocketServer($"{_configuration.BrokerHost}:{_configuration.BrokerPort}/ws");

            var options = messageBuilder.WithTls().Build();
            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(options)
                .Build();
            _mqttClient = new MqttFactory().CreateManagedMqttClient();

            await _mqttClient.StartAsync(managedOptions);
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                try
                {
                    _receivedMessage?.Invoke(
                        JsonSerializer.Deserialize<ChatMessage>(Encoding.UTF8.GetString(e.ApplicationMessage.Payload)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.StackTrace}");
                }
            });
            
            _mqttClient.UseDisconnectedHandler(e =>
            {
                Console.WriteLine($"Disconnected from MQTT Brokers pythonkindergarten.tech:15676/ws. {e.Exception.Message} : {e.Exception.InnerException}");
            });
        }
    }
}