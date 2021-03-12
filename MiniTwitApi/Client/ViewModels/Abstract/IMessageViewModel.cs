using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IMessageViewModel
    {
        public IAsyncEnumerable<MessageDTO> RequestMessages(string path);
        public string Error { get; set; }
        public DateTime GenerateDateTime(int date);

    }
}