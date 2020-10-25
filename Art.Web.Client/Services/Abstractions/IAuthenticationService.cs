using System.Threading.Tasks;
using Art.Web.Shared.Models.Person;

namespace Art.Web.Client.Services.Abstractions
{
    public interface IAuthenticationService
    {
        PersonLoginGet User { get; }

        Task Initialize();

        Task Login(string username, string password);

        Task Logout();
    }
}
