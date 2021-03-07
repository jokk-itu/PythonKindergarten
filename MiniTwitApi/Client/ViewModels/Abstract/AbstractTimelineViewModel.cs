using System.Threading.Tasks;
using Microsoft.JSInterop;
using MiniTwitApi.Shared.Models.UserModels;

namespace MiniTwitApi.Client.ViewModels.Abstract
{
    public abstract class AbstractTimelineViewModel
    {
        private readonly IJSRuntime _jsRuntime;

        protected AbstractTimelineViewModel(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        
        protected async Task<UserDTO> GetUser()
        {
            var user = await _jsRuntime.InvokeAsync<string>("getUser");
            return System.Text.Json.JsonSerializer.Deserialize<UserDTO>(user);
        }

        protected async Task<bool> IsLoggedIn()
        {
            return await _jsRuntime.InvokeAsync<bool>("isUserLoggedIn");
        }
    }
}