using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Shared.Models.Person;

namespace Art.Web.Client.Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<PersonGet>> GetAll();

        Task<PersonGet> Get(long id);

        Task Put(long id, PersonPut data);

        Task<long> Post(PersonPost data);

        Task Delete(long id);

        Task<PersonLoginGet> Login(PersonLoginPost data);
    }
}
