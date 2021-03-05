using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniTwitApi.Client.Models;
using MiniTwitApi.Client.Models.Abstract;
using MiniTwitApi.Client.ViewModels;
using MiniTwitApi.Client.ViewModels.Abstract;

namespace MiniTwitApi.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            //Register the ViewModels for the Views to use them
            builder.Services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            builder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
            
            //Register Models for the ViewModels to use them
            builder.Services.AddTransient<IUserModel, UserModel>();
            
            await builder.Build().RunAsync();
        }
    }
}
