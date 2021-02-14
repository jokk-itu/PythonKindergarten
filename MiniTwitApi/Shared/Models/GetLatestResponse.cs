using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models
{
    public class GetLatestResponse
    {
        [JsonProperty("latest")] public int Latest {get;set;}

        public GetLatestResponse(int latest)
            => Latest = latest;
    }
}