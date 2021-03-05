using System;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Client.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        public CreateUserDTO User { get; set; }
        public string RepeatPassword { get; set; }
        
        private readonly IUserModel _userModel;

        public RegisterViewModel(IUserModel userModel)
        {
            _userModel = userModel;
            User = new CreateUserDTO();
        }

        public async Task<string> RegisterUser()
        {
            ValidatePassword();
            return await _userModel.RegisterUser(User);
        }

        private void ValidatePassword()
        {
            if (!User.Password.Equals(RepeatPassword))
                throw new Exception("Passwords do not match");
        }
    }
}