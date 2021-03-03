using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models 
{
    public class CreateMessageDTO 
    {
        public CreateMessageDTO() {}

        [JsonPropertyName("author")]  
        public int Author { get; set; }

        [JsonPropertyName("authorUsername")] 
        public string AuthorUsername { get; set; }

        [JsonPropertyName("text")] 
        public string Text { get; set; }

        [JsonPropertyName("publishDate")] 
        public int PublishDate { get; set; }
        
        [JsonPropertyName("flagged")] 
        public int Flagged { get; set; }
    }
}