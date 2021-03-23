using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels.Abstract;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels
{
    public class SearchUsersViewModel : ISearchUsersViewModel
    {
        public string Input { get; set; }
        public EventCallback<ChangeEventArgs> OnInput { get; set; }
        public string Error { get; set; }

        private IUserModel _model { get; set; }

        public SearchUsersViewModel(IUserModel model)
        {
            _model = model;
        }

        public async IAsyncEnumerable<UserDTO> GetUsers()
        {
            ICollection<UserDTO> users = new List<UserDTO>();
            try
            {
                users = await _model.GetUsers(Input);
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            foreach (var user in users)
            {
                yield return user;
            }
        }
    }
}