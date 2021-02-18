using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
namespace MyApp.Entities
{
    public class Message
  {
      [Key]
      public int messageId {get; set; }
      [Required]
      public int authorId {get; set; }
      [Required]
      public User User {get; set;}
      public string authorUsername {get; set; }
      [Required]
      public string text {get; set; }
      [Required]
      public int pubDate {get; set; }
      public int flagged {get; set; }

  }
}
/*
drop table if exists message;
create table message (
  message_id integer primary key autoincrement,
  author_id integer not null,
  author_username string not null,
  text string not null,
  pub_date integer,
  flagged integer
);
*/

