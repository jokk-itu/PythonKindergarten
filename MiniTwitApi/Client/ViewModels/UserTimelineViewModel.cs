using System;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;

namespace MiniTwitApi.Client.ViewModels
{
    public class UserTimelineViewModel : IUserTimelineViewModel
    {
        public string Username { get; set; }
        public string Path { get; set; }
        public string Error { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsUnfollowed { get; set; }
        public bool FollowIsDone { get; set; }
        public bool UnFollowIsDone { get; set; }
        public int FollowsCount { get; set; }

        private readonly IFollowModel _followModel;
        private readonly IUserModel _userModel;

        public UserTimelineViewModel(IFollowModel followModel, IUserModel userModel)
        {
            _followModel = followModel;
            _userModel = userModel;
        }
        public async Task IsUserFollowedAsync()
        {
            try
            {
                var user = await _userModel.GetLoggedInUser();
                var isFollowed = await _followModel.IsFollowed(user.Username, Username);
                IsFollowed = isFollowed;
                IsUnfollowed = !isFollowed;
            }
            catch (UnauthorizedAccessException e){ /* do nothing */ }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public async Task FollowUserAsync(string whoUsername)
        {
            try
            {
                UnFollowIsDone = false;
                FollowIsDone = await _followModel.FollowUser(whoUsername, Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public async Task UnfollowUserAsync(string whoUsername)
        {
            try
            {
                FollowIsDone = false;
                UnFollowIsDone = await _followModel.UnfollowUser(whoUsername, Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public async Task RequestFollowsCountAsync()
        {
            try
            {
                FollowsCount = await _followModel.FollowerCount(Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
    }
}