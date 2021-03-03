using System.ComponentModel.DataAnnotations;

namespace MiniTwitApi.Server.Entities
{
    public class Follower
    {
        [Key]
        public int WhoId {get;set;}
        
        public User Who { get; set; }

        [Key]
        public int WhomId {get;set;}
        
        public User Whom { get; set; }
    }
}


/*
drop table if exists follower;
create table follower (
  who_id integer,
  whom_id integer
);
*/