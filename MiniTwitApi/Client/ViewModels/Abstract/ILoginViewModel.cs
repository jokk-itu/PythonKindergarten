using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface ILoginViewModel
    {
        public LoginUserDTO User { get; set; }
        UserDTO LoggedInUser { get; set; }
        public Task LoginUser();
    }
}