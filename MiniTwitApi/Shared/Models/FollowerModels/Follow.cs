using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    // {'follow': 'b'} | {'unfollow': 'b'}
    public class Follow
    {
        [JsonPropertyName("follow")] public string ToFollow { get; set; }
        [JsonPropertyName("unfollow")] public string ToUnfollow { get; set; }
    }
}