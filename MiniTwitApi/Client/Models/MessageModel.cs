using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniTwitApi.Client.Models
{
    public class MessageModel : IMessageModel
    {
        private HttpClient _client { get; set; }

        public MessageModel(HttpClient client)
        {
            _client = client;
        }

        public async IAsyncEnumerable<(MessageDTO, UserDTO)> GetMessages(string path)
        {
            var messagesFromApi = await _client.GetStringAsync(path);
            var messages = JsonSerializer.Deserialize<List<MessageDTO>>(messagesFromApi);
            if (messages is null)
                throw new Exception("No messages available");
            
            foreach(var m in messages)
            {
                var userFromApi = await _client.GetStringAsync($"/user/{m.Author}");
                var user = JsonSerializer.Deserialize<UserDTO>(userFromApi);
                yield return (m, user);
            }
        }
        
        private async Task PostMessage(MessageDTO message, string username)
        {
            var json = JsonSerializer.Serialize(message);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/msgs/{username}", data);
            var result = await response.Content.ReadAsStringAsync();
        }
    }
}