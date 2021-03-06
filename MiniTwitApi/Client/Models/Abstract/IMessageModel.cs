using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IMessageModel
    {
        public IAsyncEnumerable<(MessageDTO, UserDTO)> GetMessages(string path);
        private Task PostMessage(MessageDTO message, string username)
    }
}