using System.Dynamic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Server.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prometheus;
using Prometheus.SystemMetrics;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace MiniTwitApi.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSystemMetrics();
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddScoped<IContext, Context>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowerRepository, FollowerRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddSwaggerGen();
            
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            
            //Authentication and Authorization using Cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddAuthorizationCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IContext dbContext)
        {
            // Do database migration
            ((DbContext)dbContext).Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpMetrics();
            app.UseMetricServer();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapMetrics();
            });
        }
    }
}
