using AutoMapper;
using Ects.Persistence.Abstractions;
using Ects.Web.Api.Services.Abstractions;
using Ects.Web.Api.Services.Infrastructure;

namespace Ects.Web.Api.Services
{
    public class TestService : PersistenceServiceBase, ITestService
    {
        public TestService(IUnitOfWork uow, IMapper mapper)
            : base(uow, mapper) { }
    }
}