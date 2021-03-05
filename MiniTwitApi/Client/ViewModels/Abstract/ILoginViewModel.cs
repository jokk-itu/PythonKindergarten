using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface ILoginViewModel
    {
        public LoginUserDTO User { get; set; }
        public Task<string> LoginUser();
    }
}