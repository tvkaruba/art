using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Client.Services.Abstractions;
using Art.Web.Shared.Models.Task;
using Art.Web.Shared.Models.Variant;

namespace Art.Web.Client.Services
{
    public class VariantService : IVariantService
    {
        private readonly IHttpService _httpService;

        public VariantService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<VariantGet>> GetAllAsync()
        {
            return await _httpService.Get<IEnumerable<VariantGet>>("/api/v1/variant");
        }

        public async Task<VariantGet> GetAsync(long id)
        {
            return await _httpService.Get<VariantGet>($"/api/v1/variant/{id}");
        }

        public async Task CreateAsync(VariantPost data)
        {
            _ = await _httpService.Post<long>("/api/v1/variant", data);
        }

        public async Task UpdateAsync(long id, VariantPut data)
        {
            await _httpService.Put($"/api/v1/variant/{id}", data);
        }

        public async Task DeleteAsync(long id)
        {
            await _httpService.Delete($"api/v1/variant/{id}");
        }

        public async Task<IEnumerable<TaskGet>> GetTasksByVariantIdAsync(long variantId)
        {
            return await _httpService.Get<IEnumerable<TaskGet>>($"/api/v1/variant/{variantId}/tasks");
        }

        public async Task<IEnumerable<VariantHistoryGet>> GetVariantHistoryAsync(long variantId)
        {
            return await _httpService.Get<IEnumerable<VariantHistoryGet>>($"api/v1/variant/history/{variantId}");
        }

        public async Task<VariantExportGet> GetVariantForExportByVariantId(long variantId)
        {
            return await _httpService.Get<VariantExportGet>($"api/v1/variant/export/{variantId}");
        }
    }
}
