using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using MiniTwitApi.Client.Models;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("user");
                var user = JsonSerializer.Deserialize<UserDTO>(response);
                if (user is null)
                    return new AuthenticationState(new ClaimsPrincipal());

                var identity = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }, "serverAuth");
                
                var principal = new ClaimsPrincipal(identity);
                return new AuthenticationState(principal);
            }
            catch (Exception e)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
    }
}