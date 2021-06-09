using AutoMapper;
using Ects.Persistence.Abstractions;
using Ects.Web.Api.Services.Abstractions;
using Ects.Web.Api.Services.Infrastructure;

namespace Ects.Web.Api.Services
{
    public class ExamService : PersistenceServiceBase, IExamService
    {
        public ExamService(IUnitOfWork uow, IMapper mapper)
            : base(uow, mapper) { }
    }
}