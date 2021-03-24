using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface IChatViewModel : IViewModel
    {
        public string Message { get; set; }
        public new string Error { get; set; }
    }
}