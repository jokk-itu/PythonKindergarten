using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models
{
    // {'follow': 'b'} | {'unfollow': 'b'}
    public class Follow
    {
        [JsonProperty("follow")] public string ToFollow { get; set; }
        [JsonProperty("unfollow")] public string ToUnfollow { get; set; }
    }
}