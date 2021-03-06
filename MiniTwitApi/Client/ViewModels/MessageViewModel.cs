using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update.Internal;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class MessageViewModel : IMessageViewModel
    {
        public IAsyncEnumerable<(MessageDTO, UserDTO)> messages { get; set; }
        private readonly IMessageModel _messageModel;

        public MessageViewModel(IMessageModel messageModel)
        {
            _messageModel = messageModel;
        }

        public async Task RequestMessages(string path)
        {
            messages = _messageModel.GetMessages(path);
        }
        
        public DateTime GenerateDateTime(int date)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(date).DateTime;
        }
    }
}