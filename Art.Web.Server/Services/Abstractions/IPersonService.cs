using System.Threading.Tasks;
using Art.Web.Shared.Models.Person;
using Art.Web.Server.Services.Infrastructure.Abstractions;

namespace Art.Web.Server.Services.Abstractions
{
    public interface IPersonService : IValidatableCrudService<long, PersonPost, PersonPut, PersonGet>
    {
        Task<(string role, long id)> GetPersonRoleAsync(string email, string password);
    }
}
