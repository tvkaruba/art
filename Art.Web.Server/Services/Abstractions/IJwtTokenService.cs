namespace Art.Web.Server.Services.Abstractions
{
    public interface IJwtTokenService
    {
        string BuildToken(string role);
    }
}
