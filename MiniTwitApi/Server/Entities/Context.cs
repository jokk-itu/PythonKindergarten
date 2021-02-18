using System;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Entities
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Message> Messages { get; set; }

        public Context()
        {}

        public Context(DbContextOptions<Context> contextOptions) : base(contextOptions)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Add connection string to DB
            optionsBuilder.UseSqlite(@"../../tmp/minitwit.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne<User>(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.authorId);

            modelBuilder.Entity<Follower>().HasKey(f => new{f.WhoId, f.WhomId});
            //Add relationships between Entities

        }
    }
}