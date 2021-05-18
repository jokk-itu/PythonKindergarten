using System.Data.SqlTypes;
using System.Diagnostics;
using System.Dynamic;
using System.ComponentModel;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniTwitApi.Shared;
using Prometheus.DotNetRuntime;
using Serilog;
using Serilog.Events;

namespace MiniTwitApi.Server
{
    public class Program
    {
        //http://161.35.215.154:9200
        public static void Main(string[] args)
        {
            IDisposable collector = DotNetRuntimeStatsBuilder.Default().StartCollecting();
                
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.Console())
                .WriteTo.Async(c => c.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri($"http://{GetSecret("databaseip")}:9200")){
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}"
                }))
                .CreateLogger();

            try
            {
                Log.Information("Starting MiniTwitApi");
                CreateHostBuilder(args).Build().Run(); 
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly, {0}: {1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                Log.CloseAndFlush();
            }        
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()                        
                        .UseKestrel(options =>
                        {
                            options.Listen(IPAddress.Any, 5001, listenOptions =>
                            {

                                if(args.Any() && args[0]=="mac")
                                {
                                    Console.WriteLine("Mac isn't running");
                                }
                                else
                                {
                                    var serverCertificate = LoadCertificate();
                                    listenOptions.UseHttps(serverCertificate); // <- Configures SSL
                                }
                             
                            });
                        });
                });
                
        
        private static X509Certificate2 LoadCertificate()
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, "MiniTwitApi.Server");
            var certificateFileInfo = embeddedFileProvider.GetFileInfo("certificate.pfx");
            using (var certificateStream = certificateFileInfo.CreateReadStream())
            {
                byte[] certificatePayload;
                using (var memoryStream = new MemoryStream())
                {
                    certificateStream.CopyTo(memoryStream);
                    certificatePayload = memoryStream.ToArray();
                }

                return new X509Certificate2(certificatePayload, "");
            }
        }

        private static string GetSecret(string key)
        {
            const string DOCKER_SECRET_PATH = "/run/secrets/";
            if (Directory.Exists(DOCKER_SECRET_PATH))
            {
                IFileProvider provider = new PhysicalFileProvider(DOCKER_SECRET_PATH);
                var fileInfo = provider.GetFileInfo(key);
                if (fileInfo.Exists)
                {
                    using var stream = fileInfo.CreateReadStream();
                    using (var streamReader = new StreamReader(stream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            throw new Exception("Database IP is not available as a Docker secret");
        }
        
    }
}
