using AutoMapper;
using System.Threading.Tasks;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Web.Server.Services.Infrastructure.Abstractions;

namespace Art.Web.Server.Services.Infrastructure
{
    public abstract class CrudServiceBase<TEntity, TKey, TPost, TPut, TGet> : ReadonlyServiceBase<TEntity, TKey, TGet>, ICrudService<TKey, TPost, TPut, TGet>
        where TEntity : IEntity<TKey>
    {
        protected CrudServiceBase(IUnitOfWork uow, IMapper mapper)
            : base(uow, mapper)
        {
        }

        protected abstract Task CreateInternalAsync(TEntity entity);

        protected abstract Task UpdateInternalAsync(TEntity entity);

        protected abstract Task DeleteInternalAsync(TEntity entity);

        public virtual async Task<TGet> CreateAsync(TPost data)
        {
            var entity = Mapper.Map<TEntity>(data);
            await CreateInternalAsync(entity);

            return Mapper.Map<TGet>(entity);
        }

        public virtual async Task<TGet> UpdateAsync(TKey id, TPut data)
        {
            var existing = await GetInternalAsync(id);

            if (existing == null)
            {
                return default;
            }

            existing = Mapper.Map(data, existing);
            await UpdateInternalAsync(existing);

            return Mapper.Map<TGet>(existing);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var existing = await GetInternalAsync(id);

            if (existing != null)
            {
                await DeleteInternalAsync(existing);
            }
        }
    }
}