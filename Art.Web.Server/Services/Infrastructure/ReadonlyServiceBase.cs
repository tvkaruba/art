using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Web.Server.Services.Infrastructure.Abstractions;

namespace Art.Web.Server.Services.Infrastructure
{
    public abstract class ReadonlyServiceBase<TEntity, TKey, TGet> : PersistenceServiceBase, IReadonlyService<TKey, TGet>
        where TEntity : IEntity<TKey>
    {
        protected ReadonlyServiceBase(IUnitOfWork uow, IMapper mapper)
            : base(uow, mapper)
        {
        }

        protected abstract Task<IEnumerable<TEntity>> GetAllInternalAsync();

        protected abstract Task<TEntity> GetInternalAsync(TKey id);

        public virtual async Task<IEnumerable<TGet>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<TGet>>(await GetAllInternalAsync());
        }

        public virtual async Task<TGet> GetAsync(TKey id)
        {
            return Mapper.Map<TGet>(await GetInternalAsync(id));
        }
    }
}