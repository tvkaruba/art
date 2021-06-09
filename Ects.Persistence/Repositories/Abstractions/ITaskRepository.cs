using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Persistence.Entities;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Persistence.ReferenceData;
using ArtTask = Art.Persistence.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Persistence.Repositories.Abstractions
{
    public interface ITaskRepository : IRepository<ArtTask, long>
    {
        Task<long> QueryTasksCountWithFiltersAsync(string searchTerm, Module? module, TaskType? type);

        Task<IEnumerable<string>> QueryAllTopicsAsync();

        Task<IEnumerable<string>> QueryTopicsByTaskIdAsync(long taskId);

        Task UpdateTopicsByTaskIdAsync(long taskId, IEnumerable<string> topics);

        Task<IEnumerable<string>> QueryAllTagsAsync();

        Task<IEnumerable<string>> QueryTagsByTaskIdAsync(long taskId);

        Task UpdateTagsByTaskIdAsync(long taskId, IEnumerable<string> tags);

        Task<IEnumerable<string>> QueryAnswersByTaskIdAsync(long taskId);

        Task UpdateAnswersByTaskIdAsync(long taskId, IEnumerable<string> answers);

        Task<IEnumerable<string>> QueryRightsByTaskIdAsync(long taskId);

        Task UpdateRightsByTaskIdAsync(long taskId, IEnumerable<string> rights);

        Task<IEnumerable<ArtTask>> QueryTasksWithPaginationAndFiltersAsync(
            long page,
            long itemsOnPage,
            string searchTerm,
            Module? module,
            TaskType? type);

        Task<IEnumerable<TaskHistory>> QueryTaskHistoryByTaskIdAsync(long taskId);

        Task AddTaskConflictsAsync(long taskId, IEnumerable<long> conflictedIds);

        Task RemoveTaskConflictAsync(long firstTaskId, long secondTaskId);

        Task RemoveTaskConflictsByTaskIdAsync(long taskId);

        Task<IEnumerable<(long FirstTaskId, long SecondTaskId)>> QueryTaskConflictsAsync();
    }
}
