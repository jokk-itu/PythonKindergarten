
using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    public class GetLatestResponse
    {
        [JsonPropertyName("latest")] public int Latest {get;set;}

        public GetLatestResponse(int latest)
            => Latest = latest;
    }
}