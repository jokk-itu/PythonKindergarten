using System.Net.Http;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IUserModel
    {
        public Task<string> RegisterUser(CreateUserDTO user);
        public Task LoginUser(LoginUserDTO user);
    }
}