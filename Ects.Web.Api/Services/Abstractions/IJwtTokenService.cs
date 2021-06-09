namespace Ects.Web.Api.Services.Abstractions
{
    public interface IJwtTokenService
    {
        string BuildToken(string role);
    }
}