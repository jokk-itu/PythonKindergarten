using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Serilog;
using MiniTwitApi.Shared;

namespace MiniTwitApi.Server.Entities
{
    public class Context : DbContext, IContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Message> Messages { get; set; }
        private IConfiguration Configuration { get; set; }
        
        public Context(IConfiguration config)
        {
            Configuration = config;
        }

        public Context(DbContextOptions<Context> contextOptions) : base(contextOptions)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Add connection string to DB
                optionsBuilder.UseNpgsql(DockerSecretHelper.GetSecretOrEnvVar("db"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Make One-to-Many relationship for Users to Messages
            modelBuilder.Entity<Message>()
                .HasOne<User>(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.AuthorId);
            
            //make Many-to-Many relationship in Followers table
            modelBuilder.Entity<Follower>().HasKey(f => new {f.WhoId, f.WhomId});
            
            //make User's username unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            //relate the followers in the Users table to the followers WhoUser.
            modelBuilder.Entity<Follower>()
                .HasOne<User>(u => u.Who)
                .WithMany(u => u.Followers)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}