using System.Collections.Generic;
using MiniTwitApi.Server.Entities;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Repositories.Abstract
{
    public interface IUserRepository 
    {
        Task<bool> UserExistsAsync(string username);
        Task<bool> UserExistsAsync(int userid);
        Task CreateAsync(CreateUserDTO user);
        Task<UserDTO> ReadAsync(int userid);
        Task<UserDTO> ReadAsync(string username);
        
    }
}