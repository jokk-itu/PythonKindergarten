using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models
{
    public class MessageDTO
    {
        [JsonProperty("id")] 
        public int Id { get; set; }
        
        [JsonProperty("author")] 
        public int Author { get; set; }
        
        [JsonProperty("authorUsername")] 
        public string AuthorUsername { get; set; }
        
        [JsonProperty("text")]
        [StringLength(60, ErrorMessage = "Identifier too long (60 character limit).")] 
        public string Text { get; set; }
        
        [JsonProperty("publishDate")] 
        public int PublishDate { get; set; }
        
        [JsonProperty("flagged")] 
        public int Flagged { get; set; }
    }
}