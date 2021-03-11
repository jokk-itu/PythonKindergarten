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
            var response = await _client.GetAsync($"/fllws/?whoUserName={whoUsername}&whomUserName={whomUsername}");
            return response.IsSuccessStatusCode;
        }
    }
}