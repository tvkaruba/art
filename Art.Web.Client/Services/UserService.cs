using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Client.Services.Abstractions;
using Art.Web.Shared.Models.Person;

namespace Art.Web.Client.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<PersonGet>> GetAll()
        {
            return await _httpService.Get<IEnumerable<PersonGet>>("/api/v1/person");
        }

        public async Task<PersonGet> Get(long id)
        {
            return await _httpService.Get<PersonGet>($"/api/v1/person/{id}");
        }

        public async Task Put(long id, PersonPut data)
        {
            await _httpService.Put($"/api/v1/person/{id}", data);
        }

        public async Task<long> Post(PersonPost data)
        {
            return await _httpService.Post<long>("/api/v1/person", data);
        }

        public async Task Delete(long id)
        {
            await _httpService.Delete($"/api/v1/person/{id}");
        }

        public async Task<PersonLoginGet> Login(PersonLoginPost data)
        {
            return await _httpService.Post<PersonLoginGet>("/api/v1/person/login", data);
        }
    }
}
