using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IRegisterViewModel
    {
        public CreateUserDTO User { get; set; }
        public string RepeatPassword { get; set; }
        public string Error { get; set; }
        public bool IsRegistered { get; set; }
        
        public Task RegisterUser();
    }
}