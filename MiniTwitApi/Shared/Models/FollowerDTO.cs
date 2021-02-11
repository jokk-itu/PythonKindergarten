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
        [JsonProperty("who_id")] int WhoId { get; set; }
        [JsonProperty("whom_id")] int WhomId { get; set; }
    }
}