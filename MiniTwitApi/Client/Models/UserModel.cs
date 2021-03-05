using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;
using System.Text.Json;

namespace MiniTwitApi.Client.Models
{
    public class UserModel : IUserModel
    {
        private HttpClient Client { get; set; }
        
        public UserModel(HttpClient client)
        {
            Client = client;
        }

        public async Task<string> RegisterUser(CreateUserDTO user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/register", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task LoginUser(LoginUserDTO user)
        {
            throw new NotImplementedException();
        }
        
    }
}