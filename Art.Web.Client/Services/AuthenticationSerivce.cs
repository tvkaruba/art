using System.Threading.Tasks;
using Art.Web.Client.Services.Abstractions;
using Art.Web.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Art.Web.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;

        private readonly ILocalStorageService _localStorageService;

        private readonly IUserService _userService;

        public PersonLoginGet User { get; private set; }

        public AuthenticationService(
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IUserService userService)
        {
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _userService = userService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<PersonLoginGet>("user");
        }

        public async Task Login(string username, string password)
        {
            var user = await _userService.Login(new PersonLoginPost { Email = username, Password = password });
            await _localStorageService.SetItem("user", user);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login", forceLoad: true);
        }
    }
}
