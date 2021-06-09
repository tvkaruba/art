using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Shared.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Web.Client.Services.Abstractions
{
    public interface ITaskService
    {
        Task<long> GetTasksCountWithFilters(TaskFilters filters);

        Task<IEnumerable<TaskGet>> GetAllAsync();

        Task<TaskGet> GetAsync(long id);

        Task CreateAsync(TaskPost data);

        Task UpdateAsync(long id, TaskPut data);

        Task DeleteAsync(long id);

        Task<TaskTopicsGet> GetAllTopicsAsync();

        Task<TaskTagsGet> GetAllTagsAsync();

        Task<IEnumerable<TaskGet>> GetTasksWithPaginationAndFiltersAsync(int page, int itemsOnPage, TaskFilters filters);

        Task<IEnumerable<TaskHistoryGet>> GetTaskHistoriesByTaskIdAsync(long taskId);

        Task RemoveTaskConflicts(long firstTaskId, long secondTaskId);

        Task<IEnumerable<TaskConflictsGet>> GetTaskConflicts();
    }
}
