using Microsoft.EntityFrameworkCore;

namespace MiniTwitApi.Server.Entities
{
    public class Context : DbContext, IContext
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
            optionsBuilder.UseSqlite(@"Data Source = ../../tmp/minitwit.db");
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
        }
    }
}