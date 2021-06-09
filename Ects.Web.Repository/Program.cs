using System.Net.Http;
using System.Threading.Tasks;
using Art.Web.Client.Services;
using Art.Web.Client.Services.Abstractions;
using Ects.Web.Repository.Extensions;
using Ects.Web.Repository.Helpers;
using Ects.Web.Repository.Models;
using Ects.Web.Repository.Services;
using Ects.Web.Repository.Services.Abstractions;
using Ects.Web.Shared.QuestionTypes;
using Ects.Web.Shared.QuestionTypes.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Tewr.Blazor.FileReader;

namespace Ects.Web.Repository
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(
            //    sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMicrosoftGraphClient(
                "https://graph.microsoft.com/User.Read",
                "https://graph.microsoft.com/User.Read.All",
                "https://graph.microsoft.com/RoleManagement.Read.All",
                "https://graph.microsoft.com/GroupMember.Read.All");

            builder.Services
                .AddMsalAuthentication<RemoteAuthenticationState, LocalUserAccount>(options =>
                {
                    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                    options.UserOptions.RoleClaim = "appRole";
                })
                .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, LocalUserAccount, LocalAccountFactory>();

            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(
                    "RequireAdministratorRole",
                    policy => policy
                        .RequireAuthenticatedUser()
                        .RequireRole("appRole", "Administrator")
                        .Build());
            });

            builder.Services.AddSingleton<IQuestionType, FreeExactlyType>();
            builder.Services.AddSingleton<IQuestionType, SingleType>();
            builder.Services.AddSingleton<IQuestionType, MultipleExactlyType>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped(_ => new HttpClient());
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}