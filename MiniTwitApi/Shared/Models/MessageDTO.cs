namespace MiniTwitApi.Shared.Models
{
    /*message_id integer primary key autoincrement,
    author_id integer not null,
    text string not null,
    pub_date integer,
    flagged integer*/

    public class MessageDTO
    {
        [JsonProperty("message_id")] public int Id { get; set; }
        [JsonProperty("author_id")] public int Author { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
        [JsonProperty("pub_date")] public int PublishDate { get; set; }
        [JsonProperty("flagged")] public int Flagged { get; set; }

    }
}