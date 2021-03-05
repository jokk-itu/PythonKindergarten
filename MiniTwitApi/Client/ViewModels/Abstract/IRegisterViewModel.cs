using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IRegisterViewModel
    {
        public CreateUserDTO User { get; set; }
        public string RepeatPassword { get; set; }
        
        public Task<string> RegisterUser();
    }
}