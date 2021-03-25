using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IRegisterViewModel : IViewModel
    {
        public CreateUserDTO User { get; set; }
        public string RepeatPassword { get; set; }
        public new string Error { get; set; }
        public bool IsRegistered { get; set; }
        
        public Task RegisterUserAsync();
    }
}