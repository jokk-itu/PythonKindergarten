using System.Threading.Tasks;
using MiniTwitApi.Client.Models;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class UserTimelineViewModel : IUserTimelineViewModel
    {
        public string Username { get; set; }
        public UserDTO LoggedInUser { get; set; }
        public bool IsUserFollowed { get; set; }
        public string Path { get; set; }

        private readonly FollowModel _followModel;

        public UserTimelineViewModel(FollowModel followModel)
        {
            Path = $"msgs/{Username}";
            _followModel = followModel;
        }

        public async Task FollowUser()
        {
            await _followModel.FollowUser(Username);
        }
        
        public async Task UnfollowUser()
        {
            await _followModel.UnfollowUser(Username);
        }
    }
}