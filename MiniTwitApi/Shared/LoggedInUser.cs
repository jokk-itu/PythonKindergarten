using System;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Shared
{
    public static class LoggedInUser
    {
        public static UserDTO User {get; set;}

        public static void Login(string username) 
        {
            User = new UserDTO() 
            {
                Username = username
            };
        }   
    }

}