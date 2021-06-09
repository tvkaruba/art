using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Persistence.Entities;
using Art.Persistence.Infrastructure.Abstractions;
using ArtTask = Art.Persistence.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Persistence.Repositories.Abstractions
{
    public interface IVariantRepository : IRepository<Variant, long>
    {
        Task<IEnumerable<ArtTask>> QueryTasksByVariantIdAsync(long variantId);

        Task UpdateTasksByVariantIdAsync(long variantId, IEnumerable<long> taskIds);
    }
}
