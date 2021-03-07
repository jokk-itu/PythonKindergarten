using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    // {'content' : content}
    public class CreateMessage
    {
        [JsonPropertyName("content")] 
        public string Content { get; set; }
    }
}