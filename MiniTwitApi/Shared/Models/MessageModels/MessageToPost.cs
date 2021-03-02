using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models
{
    // {'content' : content}
    public class MessageToPost
    {
        [JsonProperty("content")] 
        public string Content { get; set; }
    }
}