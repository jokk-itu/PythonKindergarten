namespace MiniTwitApi.Shared.Models
{
    public class GetLatestResponse
    {
        [JsonProperty("latest")]
        int Latest {get;set;}

        public GetLatestResponse(int latest)
            => Latest = latest;
    }
}