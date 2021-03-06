using System;
using System.Collections.Generic;
using System.Net.Http;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;
using System.Text.Json;

namespace MiniTwitApi.Client.Models
{
    public class MessageModel : IMessageModel
    {
        private HttpClient Client { get; set; }

        public MessageModel(HttpClient client)
        {
            Client = client;
        }

        public async IAsyncEnumerable<(MessageDTO, UserDTO)> GetMessages(string path)
        {
            var messagesFromApi = await Client.GetStringAsync(path);
            var messages = JsonSerializer.Deserialize<List<MessageDTO>>(messagesFromApi);
            if (messages is null)
                throw new Exception("No messages available");
            
            foreach(var m in messages)
            {
                var userFromApi = await Client.GetStringAsync($"/user/{m.Author}");
                var user = JsonSerializer.Deserialize<UserDTO>(userFromApi);
                yield return (m, user);
            }
        }
    }
}