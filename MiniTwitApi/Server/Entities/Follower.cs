using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
namespace MyApp.Entities
{
    public class Follower
    {
        [Key]
        public int WhoId {get;set;}
        public User WhoUser {get; set;}
        [Key]
        public int WhomId {get;set;}
        public User WhomUser {get; set;}
    }
}


/*
drop table if exists follower;
create table follower (
  who_id integer,
  whom_id integer
);
*/