using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Client.Services.Abstractions;
using Art.Web.Shared.Models.Task;
using Microsoft.AspNetCore.WebUtilities;

namespace Art.Web.Client.Services
{
    public class TaskService : ITaskService
    {
        private readonly IHttpService _httpService;

        public TaskService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<TaskGet>> GetAllAsync()
        {
            return await _httpService.Get<IEnumerable<TaskGet>>("/api/v1/task");
        }

        public async Task<TaskGet> GetAsync(long id)
        {
            return await _httpService.Get<TaskGet>($"/api/v1/task/{id}");
        }

        public async Task CreateAsync(TaskPost data)
        {
            _ = await _httpService.Post<long>("/api/v1/task", data);
        }

        public async Task UpdateAsync(long id, TaskPut data)
        {
            await _httpService.Put($"/api/v1/task/{id}", data);
        }

        public async Task DeleteAsync(long id)
        {
            await _httpService.Delete($"api/v1/task/{id}");
        }

        public async Task<TaskTopicsGet> GetAllTopicsAsync()
        {
            return await _httpService.Get<TaskTopicsGet>("api/v1/task/topic");
        }

        public async Task<TaskTagsGet> GetAllTagsAsync()
        {
            return await _httpService.Get<TaskTagsGet>("api/v1/task/tag");
        }

        public async Task<long> GetTasksCountWithFilters(TaskFilters filters)
        {
            var httpFilters = new Dictionary<string, string>
            {
                ["module"] = filters.ModuleId.ToString(),
                ["type"] = filters.TaskTypeId.ToString(),
                ["sort"] = filters.SortTypeId.ToString(),
                ["searchTerm"] = filters.SearchTerm,
            };

            return await _httpService.Get<long>(
                QueryHelpers.AddQueryString("api/v1/task/count", httpFilters));
        }

        public async Task<IEnumerable<TaskGet>> GetTasksWithPaginationAndFiltersAsync(int page, int itemsOnPage, TaskFilters filters)
        {
            var httpFilters = new Dictionary<string, string>
            {
                ["module"] = filters.ModuleId.ToString(),
                ["type"] = filters.TaskTypeId.ToString(),
                ["sort"] = filters.SortTypeId.ToString(),
                ["searchTerm"] = filters.SearchTerm,
            };

            return await _httpService.Get<IEnumerable<TaskGet>>(
                QueryHelpers.AddQueryString($"api/v1/task/paged/{page}/{itemsOnPage}", httpFilters));
        }

        public async Task<IEnumerable<TaskHistoryGet>> GetTaskHistoriesByTaskIdAsync(long taskId)
        {
            return await _httpService.Get<IEnumerable<TaskHistoryGet>>($"api/v1/task/history/{taskId}");
        }

        public async Task RemoveTaskConflicts(long firstTaskId, long secondTaskId)
        {
            await _httpService.Delete($"api/v1/task/conflict/{firstTaskId}/{secondTaskId}");
        }

        public async Task<IEnumerable<TaskConflictsGet>> GetTaskConflicts()
        {
            return await _httpService.Get<IEnumerable<TaskConflictsGet>>("api/v1/task/conflict");
        }
    }
}
