using Art.Web.Server.Services.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Variant;
using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Shared.Models.Task;

namespace Art.Web.Server.Services.Abstractions
{
    public interface IVariantService : IValidatableCrudService<long, VariantPost, VariantPut, VariantGet>
    {
        Task<IEnumerable<TaskGet>> GetTasksByVariantIdAsync(long variantId);

        Task<IEnumerable<VariantHistoryGet>> GetVariantHistoryAsync(long variantId);

        Task<VariantExportGet> GetVariantForExportByVariantId(long variantId);
    }
}
