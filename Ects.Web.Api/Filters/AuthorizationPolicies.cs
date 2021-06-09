using System.Collections.Generic;

namespace Ects.Web.Api.Filters
{
    public static class AuthorizationPolicies
    {
        public static IDictionary<string, string[]> Policies { get; } = new Dictionary<string, string[]>();

        static AuthorizationPolicies()
        {
            Policies.Add(
                RolePolicies.Administrator,
                new[]
                {
                    "Administrator"
                });

            Policies.Add(RolePolicies.User,
                new[]
                {
                    "Administrator", "User", "Student"
                });
        }
    }
}