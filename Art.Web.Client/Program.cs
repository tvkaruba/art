using System;
using System.Net.Http;
using System.Threading.Tasks;
using Art.Web.Client.Services;
using Art.Web.Client.Services.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Tewr.Blazor.FileReader;

namespace Art.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IMenuService, MenuService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITaskService, TaskService>()
                .AddScoped<IVariantService, VariantService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.Configuration["apiUrl"]) })
                .AddFileReaderService(options => options.UseWasmSharedBuffer = true); ;

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();
        }
    }
}
