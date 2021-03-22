using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitChatClient.Models;

namespace MiniTwitChatClient.Abstractions
{
    public interface IMiniChatClient
    {
        Action<ChatMessage> ReceivedMessage { get; set; }
        Task PublishMessageAsync(ChatMessage message);
        Task SubscribeAsync(List<string> chatThreads);
        Task InitializeAsync();
    }
}