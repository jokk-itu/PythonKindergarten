using System.Threading.Tasks;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IUserTimelineViewModel : IViewModel
    {
        public string Username { get; set; }
        public string Path { get; set; }
        public new string Error { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsUnfollowed { get; set; }
        public bool FollowIsDone { get; set; }
        public int FollowsCount { get; set; }
        public bool UnFollowIsDone { get; set; }

        public Task UnfollowUser(string whoUsername);
        public Task FollowUser(string whoUsername);

        public Task IsUserFollowed();

        public Task RequestFollowsCount();
    }
}