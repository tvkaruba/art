using System.Collections.Generic;
using System.Threading.Tasks;
using Ects.Web.Api.Services.Infrastructure.Abstractions;
using Ects.Web.Shared.Models.Task;
using Ects.Web.Shared.Models.Variant;

namespace Ects.Web.Api.Services.Abstractions
{
    public interface IVariantService : IValidatableCrudService<long, VariantPost, VariantPut, VariantGet>
    {
        Task<IEnumerable<TaskGet>> GetTasksByVariantIdAsync(long variantId);

        Task<IEnumerable<VariantHistoryGet>> GetVariantHistoryAsync(long variantId);

        Task<VariantExportGet> GetVariantForExportByVariantId(long variantId);
    }
}