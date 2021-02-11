namespace MiniTwitApi.Shared.Models
{
    // {'follow': 'b'} | {'unfollow': 'b'}
    public class Follow
    {
        [JsonProperty("follow")] public string Follow { get; set; }
        [JsonProperty("unfollow")] public string Unfollow { get; set; }
    }
}