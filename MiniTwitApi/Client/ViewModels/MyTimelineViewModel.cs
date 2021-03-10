using System;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class MyTimelineViewModel : IMyTimelineViewModel
    {
        public CreateMessage Message { get; set; }
        public string Error { get; set; }

        public UserDTO LoggedInUser { get; set; }
        
        public bool IsMessageSent { get; set; }

        public string Path { get; set; }
        
        private readonly IMessageModel _messageModel;

        public MyTimelineViewModel(IMessageModel messageModel)
        {
            Message = new CreateMessage();
            _messageModel = messageModel;
        }

        public async Task PostMessage()
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
    }

    
}