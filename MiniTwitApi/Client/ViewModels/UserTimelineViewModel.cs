using System;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models.UserModels;

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

        private readonly IFollowModel _followModel;
        private readonly IUserModel _userModel;

        public UserTimelineViewModel(IFollowModel followModel, IUserModel userModel)
        {
            _followModel = followModel;
            _userModel = userModel;
        }
        
        public async Task IsUserFollowed()
        {
            try
            {
                //Get loggedinuser and their username
                var user = await _userModel.GetLoggedInUser();
                var _isFollowed =  await _followModel.IsFollowed(user.Username, Username);
                IsFollowed = _isFollowed;
                IsUnfollowed = !_isFollowed;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public async Task FollowUser(string whoUsername)
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

        public async Task UnfollowUser(string whoUsername)
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
    }
}