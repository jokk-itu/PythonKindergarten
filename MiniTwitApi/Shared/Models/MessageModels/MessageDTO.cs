using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models
{
    public class MessageDTO
    {
        [JsonPropertyName("id")] 
        public int Id { get; set; }
        
        [JsonPropertyName("author")] 
        public int Author { get; set; }
        
        [JsonPropertyName("authorUsername")] 
        public string AuthorUsername { get; set; }
        
        [JsonPropertyName("authorEmail")]
        public string HashedAuthorEmail { get; set; }
        
        [JsonPropertyName("text")]
        [StringLength(60, ErrorMessage = "Identifier too long (60 character limit).")] 
        public string Text { get; set; }
        
        [JsonPropertyName("publishDate")] 
        public int PublishDate { get; set; }
        
        [JsonPropertyName("flagged")] 
        public int Flagged { get; set; }
    }
}