using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IMyTimelineViewModel
    {
        public CreateMessage Message { get; set; }

        public UserDTO LoggedInUser { get; set; }

        public string Path { get; set; }
        public string Error { get; set; }

        public Task PostMessage();
    }
}