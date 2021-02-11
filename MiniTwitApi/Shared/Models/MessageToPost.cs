namespace MiniTwitApi.Shared.Models
{
    // {'content' : content}
    public class MessageToPost
    {
        [JsonProperty("content")] string Content { get; set; }
    }
}