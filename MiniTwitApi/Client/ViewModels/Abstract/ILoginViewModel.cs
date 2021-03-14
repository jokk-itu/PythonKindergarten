using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface ILoginViewModel : IViewModel
    {
        public LoginUserDTO User { get; set; }
        public UserDTO LoggedInUser { get; set; }
        
        public new string Error { get; set; }
        public Task LoginUser();
    }
}