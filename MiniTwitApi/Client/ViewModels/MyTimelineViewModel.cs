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
        public string Username { get; set; }
        
        public UserDTO LoggedInUser { get; set; }

        public string Path { get; set; }
        
        private readonly IMessageModel _messageModel;

        public MyTimelineViewModel(IMessageModel messageModel)
        {
            Path = $"msgs/{Username}";
            Message = new CreateMessage();
            _messageModel = messageModel;
        }

        public async Task PostMessage()
        {
            await _messageModel.PostMessage(Message, Username);
        }
    }

    
}