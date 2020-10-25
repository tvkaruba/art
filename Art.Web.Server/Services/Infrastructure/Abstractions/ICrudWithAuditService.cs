using System.Threading.Tasks;

namespace Art.Web.Server.Services.Infrastructure.Abstractions
{
    public interface ICrudWithAuditService<in TKey, in TPost, in TPut, TGet> : IReadonlyService<TKey, TGet>
    {
        Task<TGet> CreateAsync(TPost data);

        Task<TGet> UpdateAsync(TKey id, TPut data);

        Task DeleteAsync(TKey id);
    }
}
