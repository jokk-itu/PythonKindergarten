using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public interface ISearchUsersViewModel : IViewModel
    {
        public string Input { get; set; }
        public new string Error { get; set; }

        public IAsyncEnumerable<UserDTO> GetUsers();
    }
}