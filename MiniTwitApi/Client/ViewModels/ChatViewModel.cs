using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class ChatViewModel : IChatViewModel
    {
        public string Error { get; set; }

        public ChatViewModel()
        {
        }

    }
}