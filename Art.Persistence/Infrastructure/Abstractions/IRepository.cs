using System.Collections.Generic;
using System.Threading.Tasks;

namespace Art.Persistence.Infrastructure.Abstractions
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(TKey id);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
