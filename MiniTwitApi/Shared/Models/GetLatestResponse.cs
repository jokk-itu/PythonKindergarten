using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    public class GetLatestResponse
    {
        [JsonPropertyName("latest")] public long Latest {get;set;}

        public GetLatestResponse(long latest)
            => Latest = latest;
    }
}