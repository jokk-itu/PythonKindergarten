using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<MiniChatMQTTClient> _logger;
        private readonly IChatConfiguration _configuration; 
        private List<string> _subscribedThreads = new ();
        private IManagedMqttClient _mqttClient;

        public MiniChatMQTTClient(ILogger<MiniChatMQTTClient> logger, IChatConfiguration configuration)
        {
            _logger = logger;
            if (configuration != null)
                _configuration = configuration;
        }
        
        public async Task PublishMessageAsync(ChatMessage message)
        {
            var result = await _mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic($"threads.{message.ThreadId}")
                .WithPayload(JsonSerializer.Serialize(message))
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)1)
                .WithRetainFlag(false)
                .Build());
            
            _logger.LogInformation("Sent message to topic: {0}, with result: {1}", message.ThreadId, result.ReasonCode);
        }

        public async Task SubscribeAsync(List<string> chatThreads)
        {
            _subscribedThreads = chatThreads;

            foreach (var thread in _subscribedThreads)
                await _mqttClient.SubscribeAsync(new TopicFilterBuilder()
                    .WithTopic($"threads.{thread}")
                    .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)1)
                    .Build());
            
            _logger.LogInformation("Subscribed to {0} chat threads", chatThreads.Count);
        }
        
        public async Task ConnectAsync()
        {
            var messageBuilder = new MqttClientOptionsBuilder()
                .WithCredentials(_configuration.BrokerUser, _configuration.BrokerPassword)
                .WithWebSocketServer($"{_configuration.BrokerHost}:{_configuration.BrokerPort}/ws");

            var managedOptions = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(messageBuilder.WithTls().Build())
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
                catch (Exception)
                {
                    _logger.LogWarning("Failed to serialize incoming message...");
                }
            });

            _mqttClient.UseConnectedHandler(
                e => _logger.LogInformation("Chat client was connected to the MQTT Brokwer"));
            
            _mqttClient.UseDisconnectedHandler(e =>
                    _logger.LogCritical("Chat client was disconnected from the MQTT Broker. {0}: {1}", e.Exception.Message, e.Exception.StackTrace));
        }
    }
}