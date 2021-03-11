using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.Models
{
    public class FollowModel : IFollowModel
    {
        private readonly HttpClient _client;

        public FollowModel(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<bool> FollowUser(string myUsername, string followerUsername)
        {
            var json = JsonSerializer.Serialize(new Follow()
            {
                ToFollow = followerUsername
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/fllws/{myUsername}", data);
            HttpFailureHelper.HandleStatusCode(response);
            return response.IsSuccessStatusCode;
        }
    
        public async Task<bool> UnfollowUser(string myUsername, string followerUsername)
        {
            var json = JsonSerializer.Serialize(new Follow()
            {
                ToUnfollow = followerUsername
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/fllws/{myUsername}", data);
            HttpFailureHelper.HandleStatusCode(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsFollowed(string whoUsername, string whomUsername)
        {
            var whoUser = await _client.GetStringAsync($"/user/{whoUsername}");
            var whomUser = await _client.GetStringAsync($"/user/{whomUsername}");
            if (whoUser is null || whomUser is null)
                throw new Exception("One user does not exist");
            
            var whoUserId = JsonSerializer.Deserialize<UserDTO>(whoUser).Id;
            var whomUserId = JsonSerializer.Deserialize<UserDTO>(whomUser).Id;
            var response = await _client.GetAsync($"/fllws/findFollower/?whoUserId={whoUserId}&whomUserId={whomUserId}");
            return response.IsSuccessStatusCode;
        }
    }
}