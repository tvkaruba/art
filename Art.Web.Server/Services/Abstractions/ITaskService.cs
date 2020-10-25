using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Server.Services.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Web.Server.Services.Abstractions
{
    public interface ITaskService : IValidatableCrudService<long, TaskPost, TaskPut, TaskGet>
    {
        Task<long> GetTasksCountWithFiltersAsync(TaskFilters filters);

        Task<TaskTopicsGet> GetAllTopicsAsync();

        Task<TaskTagsGet> GetAllTagsAsync();

        Task<IEnumerable<TaskGet>> GetTasksWithPaginationAndFiltersAsync(int page, int itemsOnPage, TaskFilters filters);

        Task<IEnumerable<TaskHistoryGet>> GetTaskHistoriesByTaskIdAsync(long taskId);

        Task RemoveTaskConflictsAsync(long firstTaskId, long secondTaskId);

        Task<IEnumerable<TaskConflictsGet>> GetTaskConflictsAsync();
    }
}
