using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class MessageViewModel : IMessageViewModel
    {
        private readonly IMessageModel _messageModel;

        public MessageViewModel(IMessageModel messageModel)
        {
            _messageModel = messageModel;
        }

        public async IAsyncEnumerable<(MessageDTO, UserDTO)> RequestMessages(string path)
        {
            await foreach (var m in _messageModel.GetMessages(path))
            {
                yield return m;
            }
        }
        
        public DateTime GenerateDateTime(int date)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(date).DateTime;
        }
    }
}