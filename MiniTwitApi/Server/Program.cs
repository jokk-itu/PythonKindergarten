using System.Data.SqlTypes;
using System.Diagnostics;
using System.Dynamic;
using System.ComponentModel;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniTwitApi.Shared;

namespace MiniTwitApi.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length > 0)
                DeleteMe.TestRun = args[0] == "test";

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    if(args.Length > 0 && args[0] == "test")
                        webBuilder.UseUrls(new string[]{"https://0.0.0.0:5001", "http://0.0.0.0:5000"});
                    else
                        webBuilder.UseUrls(new string[]{"https://165.227.161.247:443", "http://165.227.161.247:80"});
                        
                });
    }
}
