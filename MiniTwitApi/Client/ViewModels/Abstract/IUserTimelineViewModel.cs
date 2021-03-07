using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IUserTimelineViewModel
    {
        public string Username { get; set; }
        public UserDTO LoggedInUser { get; set; }
        public bool IsUserFollowed { get; set; }
        public string Path { get; set; }

        public Task UnfollowUser();
        public Task FollowUser();
    }
}