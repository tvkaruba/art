using System.Threading.Tasks;
using Ects.Web.Api.Services.Infrastructure.Abstractions;
using Ects.Web.Shared.Models.Person;

namespace Ects.Web.Api.Services.Abstractions
{
    public interface IPersonService : IValidatableCrudService<long, PersonPost, PersonPut, PersonGet>
    {
        Task<(string role, long id)> GetPersonRoleAsync(string email, string password);
    }
}