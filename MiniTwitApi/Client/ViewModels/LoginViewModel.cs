using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public LoginUserDTO User { get; set; }
        private readonly IUserModel _userModel;

        public LoginViewModel(IUserModel userModel)
        {
            _userModel = userModel;
            User = new LoginUserDTO();
        }

        public async Task LoginUser()
        {
            await _userModel.LoginUser(User);
        }
    }
}