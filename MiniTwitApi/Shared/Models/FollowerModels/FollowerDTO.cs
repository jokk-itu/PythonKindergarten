using System.Text.Json.Serialization;

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
        [JsonPropertyName("who_id")] 
        public int WhoId { get; set; }
        [JsonPropertyName("whom_id")] 
        public int WhomId { get; set; }
    }
}