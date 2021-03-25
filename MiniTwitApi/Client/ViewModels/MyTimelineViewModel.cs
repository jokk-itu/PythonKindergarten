using System;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class MyTimelineViewModel : IMyTimelineViewModel
    {
        public CreateMessage Message { get; set; }
        public UserDTO LoggedInUser { get; set; }
        public string Error { get; set; }
        public bool IsMessageSent { get; set; }
        public string Path { get; set; }
        
        private readonly IMessageModel _messageModel;
        private readonly IUserModel _userModel;

        public MyTimelineViewModel(IMessageModel messageModel, IUserModel userModel)
        {
            Message = new CreateMessage();
            _messageModel = messageModel;
            _userModel = userModel;
        }

        public async Task PostMessageAsync()
        {
            try
            {
                IsMessageSent = await _messageModel.PostMessage(Message, LoggedInUser.Username);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        public async Task<UserDTO> GetLoggedInUserAsync()
        {
            return await _userModel.GetLoggedInUser();
        }
    }

    
}