using System.ComponentModel.DataAnnotations;

namespace MiniTwitApi.Server.Entities
{ 
    public class Message
    {
          [Key]
          public int MessageId {get; set; }
          [Required]
          public int AuthorId {get; set; }
          [Required]
          public User User {get; set;}
          public string AuthorUsername {get; set; }
          [Required]
          public string Text {get; set; }
          [Required]
          public int PubDate {get; set; }
          public int Flagged {get; set; }
    }
}

