using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Shared.Repositories.Abstractions
{
    public interface IMiniTwitRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<IList<MessageDTO>> QueryMessagesAsync(string userId, int limit);
        Task<List<MessageDTO>> QueryMessagesAsync(int limit);
        Task InsertUserAsync(UserDTO user);
        Task InsertMessageAsync(MessageDTO message);
        Task<UserDTO> QueryUserByIdAsync(int userId);
        Task<UserDTO> QueryUserByUsernameAsync(string username);
        Task InsertFollowAsync(FollowerDTO follow);
        Task RemoveFollowAsync(FollowerDTO follow);
        Task<IList<FollowerDTO>> QueryFollowers(string userId, int limit = 20);
    }
}