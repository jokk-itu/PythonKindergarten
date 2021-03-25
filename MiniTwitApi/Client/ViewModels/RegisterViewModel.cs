using System;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models.UserModels;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Client.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        public CreateUserDTO User { get; set; }
        public string Error { get; set; }
        public bool IsRegistered { get; set; }
        public string RepeatPassword { get; set; }
        private readonly IUserModel _userModel;

        public RegisterViewModel(IUserModel userModel)
        {
            _userModel = userModel;
            User = new CreateUserDTO();
        }

        public async Task RegisterUserAsync()
        {
            try
            {
                ValidatePassword();
                IsRegistered = await _userModel.RegisterUser(User);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        private void ValidatePassword()
        {
            if (!User.Password.Equals(RepeatPassword))
                throw new Exception("Passwords do not match");
        }
    }
}