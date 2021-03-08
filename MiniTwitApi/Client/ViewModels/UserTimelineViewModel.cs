using System;
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
        public string Path { get; set; }
        public string Error { get; set; }
        
        public bool IsFollowed { get; set; }

        public bool IsUnfollowed { get; set; }

        private readonly IFollowModel _followModel;

        public UserTimelineViewModel(IFollowModel followModel)
        {
            _followModel = followModel;
        }
        
        public async Task<bool> CheckIfUserIsFollowed()
        {
            try
            {
                return await _followModel.IsFollowed(LoggedInUser.Username, Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }

        public async Task FollowUser()
        {
            try
            {
                IsFollowed = await _followModel.FollowUser(LoggedInUser.Username, Username);
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
                IsUnfollowed = await _followModel.UnfollowUser(LoggedInUser.Username, Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }
    }
}