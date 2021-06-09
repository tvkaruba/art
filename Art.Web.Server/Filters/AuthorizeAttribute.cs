using System;
using System.Linq;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Art.Web.Server.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _policy;

        public AuthorizeAttribute() : this(RolePolicies.User)
        {
        }

        public AuthorizeAttribute(string policy)
        {
            _policy = policy;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = (string)context.HttpContext.Items["Role"];

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new AuthenticationException("Unauthorized");
            }

            if (!AuthorizationPolicies.Policies[_policy].Contains(role))
            {
                throw new AuthenticationException("Unauthorized");
            }
        }
    }
}
