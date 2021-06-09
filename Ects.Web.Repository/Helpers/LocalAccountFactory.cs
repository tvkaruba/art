using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Ects.Web.Repository.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace Ects.Web.Repository.Helpers
{
    public class LocalAccountFactory : AccountClaimsPrincipalFactory<LocalUserAccount>
    {
        private readonly IServiceProvider _serviceProvider;

        public LocalAccountFactory(
            IAccessTokenProviderAccessor accessor,
            IServiceProvider serviceProvider)
            : base(accessor)
        {
            _serviceProvider = serviceProvider;
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            LocalUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity is { IsAuthenticated: true })
            {
                var userIdentity = (ClaimsIdentity) initialUser.Identity;

                foreach (var role in account.Roles) userIdentity.AddClaim(new Claim("appRole", role));

                foreach (var wid in account.Wids) userIdentity.AddClaim(new Claim("directoryRole", wid));

                try
                {
                    var graphClient = ActivatorUtilities
                        .CreateInstance<GraphServiceClient>(_serviceProvider);

                    var requestMe = graphClient.Me.Request();
                    var user = await requestMe.GetAsync();

                    var requestMemberOf = graphClient.Users[account.Oid].MemberOf;
                    var memberships = await requestMemberOf.Request().GetAsync();

                    if (memberships != null)
                        foreach (var entry in memberships)
                            if (entry.ODataType == "#microsoft.graph.group")
                                userIdentity.AddClaim(
                                    new Claim("directoryGroup", entry.Id));
                }
                catch (AccessTokenNotAvailableException e)
                {
                    // TODO: Logger
                }
            }

            return initialUser;
        }
    }
}