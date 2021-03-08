using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public LoginUserDTO User { get; set; }
        public UserDTO LoggedInUser { get; set; }
        private readonly IUserModel _userModel;

        public LoginViewModel(IUserModel userModel)
        {
            _userModel = userModel;
            User = new LoginUserDTO();
        }

        public async Task LoginUser()
        {
            if (await _userModel.LoginUser(User))
                LoggedInUser = new UserDTO()
                {
                    Username = User.Username,
                    Email = User.Email
                };
        }
    }
}