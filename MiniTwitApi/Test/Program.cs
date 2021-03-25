using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MiniTwitChatClient;
using MiniTwitChatClient.Abstractions;
using MiniTwitChatClient.Models;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IMiniChatClient client = new MiniChatMQTTClient(LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger<MiniChatMQTTClient>(), new ChatConfiguration("pythonkindergarten.tech", 15676,  "minitwit", "minitwit"));
            await client.ConnectAsync();
            await client.SubscribeAsync(new List<string>()
            {
                "testing",
                "2363"
            });

            client.ReceivedMessage += message =>
                Console.WriteLine($"Received message on thread {message.ThreadId}, content: {message.Content}");

            while (true)
            {
                var msg = Console.ReadLine();

                if (msg.Contains(":"))
                {
                    var message = new ChatMessage()
                    {
                        ThreadId = msg.Split(':')[0],
                        Content = msg.Split(':')[1],
                        Sender = "TestConsole"
                    };

                    await client.PublishMessageAsync(message); //message);
                }
            }
        }
    }
}