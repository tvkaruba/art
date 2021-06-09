using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Art.Web.Server.Filters
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IConfiguration _configuration;

        public JwtAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next ?? throw new ArgumentException(nameof(next));
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers[HeaderNames.Authorization].FirstOrDefault()
                ?.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();

            if (token != null)
            {
                new JwtSecurityTokenHandler().ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        // Set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later).
                        ClockSkew = TimeSpan.Zero
                    },
                    out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var role = jwtToken.Claims.Single(c => c.Type == JwtRegisteredClaimNames.GivenName).Value;

                if (!string.IsNullOrWhiteSpace(role))
                {
                    // Attach role to context on successful jwt validation.
                    context.Items["Role"] = role;
                }
            }

            await _next(context);
        }
    }
}
