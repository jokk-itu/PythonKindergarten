using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Client.Models
{
    public class FollowModel : IFollowModel
    {
        private readonly HttpClient _client;

        public FollowModel(HttpClient client)
        {
            _client = client;
        }
        
        public async Task FollowUser(string username)
        {
            var json = JsonSerializer.Serialize(new Follow()
            {
                ToFollow = username
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/fllws", data);
            //Handle Errors
        }
    
        public async Task UnfollowUser(string username)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new Follow()
            {
                ToUnfollow = username
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/fllws", data);
            //Handle Errors
        }
    }
}