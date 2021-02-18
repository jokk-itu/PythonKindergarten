using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
namespace MyApp.Entities
{
    public class User
    {
        public int UserId {get;set;}
        [Required]
        public string Username {get;set;}
        [Required]
        public string Email{get;set;}
        [Required]
        public string PwHash{get;set;}
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