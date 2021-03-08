using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IMessageModel
    {
        public IAsyncEnumerable<MessageDTO> GetMessages(string path);
        public Task PostMessage(CreateMessage message, string username);
    }
}