using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MiniTwitApi.Tester
{
    class Program
    {
        private static HttpClient _client;

        static async Task Main(string[] args)
        {
            var random = new Random();
            var clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandler.ServerCertificateCustomValidationCallback = 
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            
            _client = new HttpClient(clientHandler);

            var victor = "Victor" + random.Next(100, 9999);
            var anne = "Anne" + random.Next(100, 9999);
            var joachim = "Joachim" + random.Next(100, 9999);
            var isabella = "Isabella" + random.Next(100, 9999);
            var bjornar = "Bjornar" + random.Next(100, 9999);
            
            Console.WriteLine("Creating accounts....");
            var createdVictor = await CreateAccount(victor, "somethingsupersafe", "vino@itu.dk");
            Console.WriteLine((createdVictor ? "Created" : "Failed to create") + " Victor");
            var createdAnne = await CreateAccount(anne, "somethingsupersafe", "asie@itu.dk");
            Console.WriteLine((createdAnne ? "Created" : "Failed to create") + " Anne");
            var createdJoachim = await CreateAccount(joachim, "somethingsupersafe", "jokk@itu.dk");
            Console.WriteLine((createdJoachim ? "Created" : "Failed to create") + " Joachim");
            var createdIsabella = await CreateAccount(isabella, "somethingsupersafe", "iras@itu.dk");
            Console.WriteLine((createdIsabella ? "Created" : "Failed to create") + " Isabella");
            var createdBjornar = await CreateAccount(bjornar, "somethingsupersafe", "bjjr@itu.dk");
            Console.WriteLine((createdBjornar ? "Created" : "Failed to create") + " Bjornar");
            
            Console.WriteLine("Posting with Victor");
            var postedVictor = await PostMessage(victor, "Please don't abuse. ");
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");
            
            Console.WriteLine("Posting with Anne");
            var postedAnne = await PostMessage(anne, "I am not creative.");
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");
            
            Console.WriteLine("Posting with Joachim");
            var postedJoachim = await PostMessage(joachim, "Halloumi.");
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");
            
            Console.WriteLine("Posting with Isabella");
            var postedIsabella = await PostMessage(isabella, "I want to sleep.");
            
            Console.WriteLine("Posting with Bjornar");
            var postedBjornar = await PostMessage(bjornar, "Godt spørgsmål??");
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");
        }

        private static async Task<bool> CreateAccount(string username, string password, string email){
            var request = new HttpRequestMessage(){
                RequestUri = new Uri("https://pythonkindergarten.tech/register"),
                Method = HttpMethod.Post,
                Content = new StringContent($"{{\n  \"username\": \"{username}\",\n  \"pwd\": \"{password}\",\n  \"email\": \"{email}\"\n}}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return (await _client.SendAsync(request)).IsSuccessStatusCode;
        }

        private static async Task<bool> PostMessage(string username, string message)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://pythonkindergarten.tech/msgs/{username}"),
                Method = HttpMethod.Post,
                Content = new StringContent($"{{\n  \"content\": \"{message}\"\n}}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return (await _client.SendAsync(request)).IsSuccessStatusCode;
        }
    }
}