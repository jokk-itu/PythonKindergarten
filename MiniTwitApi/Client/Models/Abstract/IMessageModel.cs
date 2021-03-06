using System.Collections.Generic;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IMessageModel
    {
        public IAsyncEnumerable<(MessageDTO, UserDTO)> GetMessages(string path);
    }
}