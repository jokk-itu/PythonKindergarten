using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
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

        public async Task<bool> RegisterUser(CreateUserDTO user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/register", data);
            HttpFailureHelper.HandleStatusCode(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUser(LoginUserDTO user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/login", data);
            HttpFailureHelper.HandleStatusCode(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<UserDTO> GetLoggedInUser()
        {
            var response = await Client.GetAsync("user");
            HttpFailureHelper.HandleStatusCode(response);
            return JsonSerializer.Deserialize<UserDTO>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ICollection<UserDTO>> GetUsers(string input)
        {
            var response = await Client.GetAsync($"user/search/?input={input}");
            HttpFailureHelper.HandleStatusCode(response);
            return JsonSerializer.Deserialize<ICollection<UserDTO>>(await response.Content.ReadAsStringAsync());
        }
    }
}