using AutoMapper;
using System;
using Art.Persistence.Infrastructure.Abstractions;

namespace Art.Web.Server.Services.Infrastructure
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