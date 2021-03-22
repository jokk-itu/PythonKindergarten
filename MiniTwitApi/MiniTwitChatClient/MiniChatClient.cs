using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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

        private readonly IModel _rabbitClient;
        private readonly ChatConfiguration _configuration;
        private Action<ChatMessage> _receivedMessage;
        private EventingBasicConsumer _rabbitConsumer;

        public MiniChatClient(ChatConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = configuration.BrokerHost, 
                Port = configuration.BrokerPort,
                UserName = configuration.BrokerUser, 
                Password = configuration.BrokerPassword
            };
            _rabbitClient = factory.CreateConnection().CreateModel();
        }

        public Task PublishMessageAsync(ChatMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _rabbitClient.BasicPublish("", message.ThreadId, null, body);
            return Task.FromResult(0);
        }

        public Task SubscribeAsync(List<string> chatThreads)
        {
            foreach (var thread in chatThreads)
            {
                _rabbitClient.QueueDeclare(thread, false, false, true, null);
                _rabbitClient.BasicConsume(thread, true, _rabbitConsumer);   
            }

            return Task.FromResult(0);
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        private void HandleReceivedJob(object obj, BasicDeliverEventArgs eventArgs)
            => _receivedMessage?.Invoke(JsonSerializer.Deserialize<ChatMessage>(Encoding.UTF8.GetString(eventArgs.Body.ToArray())));     
    }
}