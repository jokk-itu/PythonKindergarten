using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    public class FollowerDTO
    {
        [JsonPropertyName("who_id")] 
        public int WhoId { get; set; }
        [JsonPropertyName("whom_id")] 
        public int WhomId { get; set; }
    }
}