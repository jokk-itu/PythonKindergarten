using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MiniTwitChatClient.Abstractions;
using MiniTwitChatClient.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MiniTwitChatClient
{
    public class MiniChatClient : IMiniChatClient
    {
        public Action<ChatMessage> ReceivedMessage
        {
            get => _receivedMessage;
            set
            {
                if (_rabbitConsumer == null)
                {
                    _rabbitConsumer = new EventingBasicConsumer(_rabbitClient);
                    _rabbitConsumer.Received += HandleReceivedJob;
                }

                _receivedMessage = value;
            }
        }

        private readonly ILogger<MiniChatClient> _logger;
        private readonly IChatConfiguration _configuration;
        private IModel _rabbitClient;
        private Action<ChatMessage> _receivedMessage;
        private EventingBasicConsumer _rabbitConsumer;

        public MiniChatClient(ILogger<MiniChatClient> logger, IChatConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task PublishMessageAsync(ChatMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _rabbitClient.BasicPublish("", message.ThreadId, null, body);
            
            _logger.LogInformation("Sent message to topic: {0}", message.ThreadId);
            return Task.FromResult(0);
        }

        public Task SubscribeAsync(List<string> chatThreads)
        {
            foreach (var thread in chatThreads)
            {
                _rabbitClient.QueueDeclare(thread, false, false, true, null);
                _rabbitClient.BasicConsume(thread, true, _rabbitConsumer);   
            }

            _logger.LogInformation("Subscribed to {0} chat threads", chatThreads.Count);
            return Task.FromResult(0);
        }

        public Task ConnectAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration.BrokerHost, 
                Port = _configuration.BrokerPort,
                UserName = _configuration.BrokerUser, 
                Password = _configuration.BrokerPassword
            };
            _rabbitClient = factory.CreateConnection().CreateModel();

            _logger.LogInformation("Chat client was connected to the MQTT Broker");
            return Task.FromResult(0);
        }

        private void HandleReceivedJob(object obj, BasicDeliverEventArgs eventArgs)
            => _receivedMessage?.Invoke(JsonSerializer.Deserialize<ChatMessage>(Encoding.UTF8.GetString(eventArgs.Body.ToArray())));     
    }
}