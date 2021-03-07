using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using MiniTwitApi.Client.Models;
using MiniTwitApi.Client.Models.Abstract;
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

        private readonly IFollowModel _followModel;

        public UserTimelineViewModel(IFollowModel followModel)
        {
            Path = $"msgs/{Username}";
            _followModel = followModel;
        }

        public async Task FollowUser()
        {
            await _followModel.FollowUser(LoggedInUser.Username, Username);
        }
        
        public async Task UnfollowUser()
        {
            await _followModel.UnfollowUser(LoggedInUser.Username, Username);
        }
    }
}