using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IUserTimelineViewModel
    {
        public string Username { get; set; }
        public UserDTO LoggedInUser { get; set; }
        public string Path { get; set; }
        public string Error { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsUnfollowed { get; set; }
        public bool FollowIsDone { get; set; }
        public bool UnFollowIsDone { get; set; }

        public Task UnfollowUser();
        public Task FollowUser();

        public Task CheckIfUserIsFollowed();
    }
}