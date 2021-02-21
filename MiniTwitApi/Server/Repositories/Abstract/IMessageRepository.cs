using System.Collections.Generic;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Shared.Models;
using System.Threading.Tasks;

namespace MiniTwitApi.Server.Repositories.Abstract
{
    public interface IMessageRepository 
    {
        Task CreateAsync(MessageDTO message);
        Task<ICollection<MessageDTO>> ReadAllAsync(int limit);
        Task<ICollection<MessageDTO>> ReadAllUserAsync(string username, int limit);
    }
}
