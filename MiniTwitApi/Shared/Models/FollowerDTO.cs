using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models
{
    /*
    create table follower (
  who_id integer,
  whom_id integer
);
  */
    public class FollowerDTO
    {
        [JsonProperty("who_id")] public int WhoId { get; set; }
        [JsonProperty("whom_id")] public int WhomId { get; set; }
    }
}