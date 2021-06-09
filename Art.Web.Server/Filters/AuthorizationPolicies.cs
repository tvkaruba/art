using System.Collections.Generic;
using Art.Persistence.ReferenceData;

namespace Art.Web.Server.Filters
{
    public static class AuthorizationPolicies
    {
        static AuthorizationPolicies()
        {
            Policies.Add(
                RolePolicies.Administrator,
                new[]
                {
                    nameof(PersonRole.Administrator),
                });

            Policies.Add(RolePolicies.User,
                new []
                {
                    nameof(PersonRole.Administrator),
                    nameof(PersonRole.User),
                });
        }

        public static IDictionary<string, string[]> Policies { get; } = new Dictionary<string, string[]>();
    }
}
