using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Shared.Models.Task;
using Art.Web.Shared.Models.Variant;

namespace Art.Web.Client.Services.Abstractions
{
    public interface IVariantService
    {
        Task<IEnumerable<VariantGet>> GetAllAsync();

        Task<VariantGet> GetAsync(long id);

        Task CreateAsync(VariantPost data);

        Task UpdateAsync(long id, VariantPut data);

        Task DeleteAsync(long id);

        Task<IEnumerable<TaskGet>> GetTasksByVariantIdAsync(long variantId);

        Task<IEnumerable<VariantHistoryGet>> GetVariantHistoryAsync(long variantId);

        Task<VariantExportGet> GetVariantForExportByVariantId(long variantId);
    }
}
