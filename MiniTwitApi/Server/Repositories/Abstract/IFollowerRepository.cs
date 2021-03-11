using System.Collections.Generic;
using MiniTwitApi.Server.Entities;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Repositories.Abstract
{
    public interface IFollowerRepository 
    {
        Task<ICollection<FollowerDTO>> ReadAllAsync(string username);
        Task<int> DeleteAsync(FollowerDTO follower);
        Task<int> CreateAsync(FollowerDTO follower);
        Task<Follower> ReadAsync(string whoUsername, string whomUsername);

    }
}