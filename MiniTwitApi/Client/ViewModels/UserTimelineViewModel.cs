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
        public UserDTO LoggedInUser { get; set; }
        public string Path { get; set; }
        public string Error { get; set; }
        
        public bool IsFollowed { get; set; }

        public bool IsUnfollowed { get; set; }
        
        public bool FollowIsDone { get; set; }
        
        public bool UnFollowIsDone { get; set; }

        private readonly IFollowModel _followModel;

        public UserTimelineViewModel(IFollowModel followModel)
        {
            _followModel = followModel;
        }
        
        public async Task CheckIfUserIsFollowed()
        {
            try
            {
                var _isFollowed =  await _followModel.IsFollowed(LoggedInUser.Username, Username);
                IsFollowed = _isFollowed;
                IsUnfollowed = !_isFollowed;
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public async Task FollowUser()
        {
            try
            {
                FollowIsDone = await _followModel.FollowUser(LoggedInUser.Username, Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
        
        public async Task UnfollowUser()
        {
            try
            {
                UnFollowIsDone = await _followModel.UnfollowUser(LoggedInUser.Username, Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
    }
}