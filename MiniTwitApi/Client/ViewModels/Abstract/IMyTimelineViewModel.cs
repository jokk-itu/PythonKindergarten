using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IMyTimelineViewModel : IViewModel
    {
        public CreateMessage Message { get; set; }

        public UserDTO LoggedInUser { get; set; }
        
        public bool IsMessageSent { get; set; }

        public string Path { get; set; }
        public new string Error { get; set; }

        public Task PostMessage();
    }
}