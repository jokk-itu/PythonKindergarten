using System.Net.Http;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IUserModel
    {
        public Task<bool> RegisterUser(CreateUserDTO user);
        public Task<bool> LoginUser(LoginUserDTO user);

        public Task<UserDTO> GetLoggedInUser();
    }
}