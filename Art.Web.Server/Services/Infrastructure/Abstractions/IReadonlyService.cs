using System.Collections.Generic;
using System.Threading.Tasks;

namespace Art.Web.Server.Services.Infrastructure.Abstractions
{
    public interface IReadonlyService<in TKey, TGet>
    {
        Task<IEnumerable<TGet>> GetAllAsync();

        Task<TGet> GetAsync(TKey id);
    }
}
