using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ects.Web.Api.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ects.Web.Api.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Builds the token used for authentication.
        /// </summary>
        /// <param name="role">Users role.</param>
        /// <returns>JWT token.</returns>
        public string BuildToken(string role)
        {
            var handler = new JwtSecurityTokenHandler();

            // Create a claim based on the users email. You can add more claims like ID's and any other info.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.GivenName, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims);

            // Creates a key from our private key that will be used in the security algorithm next.
            // Credentials that are encrypted which can only be created by our server using the private key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var notBefore = DateTime.UtcNow;
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpireTime"]));

            // This is the actual token that will be issued to the user.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds,
                Subject = identity,
                NotBefore = notBefore,
                Expires = expires
            };
            var securityToken = handler.CreateToken(tokenDescriptor);

            // Return the token in the correct format using JwtSecurityTokenHandler.
            return handler.WriteToken(securityToken);
        }
    }
}