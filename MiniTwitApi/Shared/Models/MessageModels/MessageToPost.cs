using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    // {'content' : content}
    public class MessageToPost
    {
        [JsonPropertyName("content")] 
        public string Content { get; set; }
    }
}