using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    public class Follow
    {
        [JsonPropertyName("follow")] public string ToFollow { get; set; }
        [JsonPropertyName("unfollow")] public string ToUnfollow { get; set; }
    }
}