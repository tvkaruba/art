using System;
using AutoMapper;
using Ects.Persistence.Abstractions;

namespace Ects.Web.Api.Services.Infrastructure
{
    public class PersistenceServiceBase : IDisposable
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected readonly IMapper Mapper;

        protected PersistenceServiceBase(IUnitOfWork uow, IMapper mapper)
        {
            UnitOfWork = uow;
            Mapper = mapper;
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}