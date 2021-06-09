using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Ects.Web.Repository.Pages.Components
{
    public abstract class UserClaimsBase : ComponentBase
    {
        private readonly string[] _returnClaims = { "oid", "appRole" };

        protected string AuthMessage;

        protected IEnumerable<Claim> Claims = Enumerable.Empty<Claim>();

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetClaimsPrincipalData();
        }

        private async Task GetClaimsPrincipalData()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: true })
            {
                AuthMessage = $"{user.Identity.Name} is authenticated.";
                Claims = user.Claims.Where(_ => _returnClaims.Contains(_.Type));
            }
            else
            {
                AuthMessage = "The user is NOT authenticated.";
            }
        }
    }
}