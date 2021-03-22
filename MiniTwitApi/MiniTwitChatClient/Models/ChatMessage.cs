using System;
using System.Text.Json.Serialization;

namespace MiniTwitChatClient.Models
{
    public class ChatMessage
    {
        [JsonPropertyName("threadId")] public string ThreadId { get; set; }
        [JsonPropertyName("sender")] public string Sender { get; set; }
        [JsonPropertyName("content")] public string Content { get; set; }
        [JsonPropertyName("timestamp")] public DateTime Timestamp { get; set; }
    }
}