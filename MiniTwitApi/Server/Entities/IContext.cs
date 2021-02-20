using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitApi.Server.Entities
{
    public interface IContext
    {
        public DbSet<Follower> Followers { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Message> Messages { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}