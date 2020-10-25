using System.Threading.Tasks;
using Art.Persistence.Entities;
using Art.Persistence.Infrastructure.Abstractions;

namespace Art.Persistence.Repositories.Abstractions
{
    public interface IPersonRepository : IRepository<Person, long>
    {
        Task<Person> QueryPersonByEmailAsync(string email);
    }
}
