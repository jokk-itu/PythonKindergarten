using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Shared.Models.UserModels;
using MiniTwitApi.Shared;
using System.Text.Json;
using System;

namespace MiniTwitApi.Client.Models
{
    public class UserModel : IUserModel
    {
        private HttpClient Client { get; set; }
        
        public UserModel(HttpClient client)
        {
            Client = client;
        }

        public async Task RegisterUser(CreateUserDTO user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/register", data);
            HttpFailureHelper.HandleStatusCode(response);
        }

        public async Task<bool> LoginUser(LoginUserDTO user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/login", data);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            HttpFailureHelper.HandleStatusCode(response);
            return false;
        }
    }
}