using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniTwitApi.Server.Entities
{
    public class User
    {
        [Key]
        public int UserId {get; set;}
        
        [Required]
        public string Username {get; set;}
        
        [Required]
        public string Email{get; set;}
        
        [Required]
        public string Password{get; set;}
        
        public ICollection<Follower> Followers { get; set; }
        
        public ICollection<Message> Messages { get; set; }
    }
}

/*
drop table if exists user;
create table user (
  user_id integer primary key autoincrement,
  username string not null,
  email string not null,
  pw_hash string not null
);
*/