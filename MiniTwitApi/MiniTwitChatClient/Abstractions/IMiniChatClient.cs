using System;
using MiniTwitChatClient.Models;

namespace MiniTwitChatClient.Abstractions
{
    public interface IMiniChatClient
    {
        Action<ChatMessage> ReceivedMessage { get; set; }
        void PublishMessage(ChatMessage message);
    }
}