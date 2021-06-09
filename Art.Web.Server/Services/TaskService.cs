using Art.Web.Server.Services.Abstractions;
using Art.Web.Server.Services.Infrastructure;
using Art.Web.Shared.Models.Task;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Web.Server.Extensions;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Common;
using AutoMapper;
using ArtTask = Art.Persistence.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Web.Server.Services
{
    public class TaskService : ValidatableCrudWithAuditServiceBase<ArtTask, long, TaskPost, TaskPut, TaskGet>, ITaskService
    {
        private readonly IValidationService<TaskFilters> _filtersValidationService;

        public TaskService(
            IUnitOfWork uow,
            IMapper mapper,
            IValidationService<TaskPost> createValidationService,
            IValidationService<TaskPut> updateValidationService,
            IValidationService<TaskFilters> filtersValidationService)
            : base(uow, mapper, createValidationService, updateValidationService)
        {
            _filtersValidationService = filtersValidationService;
        }

        public async Task<TaskTagsGet> GetAllTagsAsync()
        {
            return new TaskTagsGet { Tags = await UnitOfWork.TaskRepository.QueryAllTagsAsync() };
        }

        public async Task<TaskTopicsGet> GetAllTopicsAsync()
        {
            return new TaskTopicsGet { Topics = await UnitOfWork.TaskRepository.QueryAllTopicsAsync() };
        }

        public async Task<long> GetTasksCountWithFiltersAsync(TaskFilters filters)
        {
            await _filtersValidationService.ValidateAsync(filters, CancellationToken.None);

            var tasks = new List<TaskGet>();
            return await UnitOfWork.TaskRepository
                .QueryTasksCountWithFiltersAsync(
                    string.IsNullOrEmpty(filters.SearchTerm)
                        ? "%"
                        : filters.SearchTerm.ConvertToSqlLikePattern(),
                    filters.ModuleId,
                    filters.TaskTypeId);
        }

        public async Task<IEnumerable<TaskGet>> GetTasksWithPaginationAndFiltersAsync(int page, int itemsOnPage, TaskFilters filters)
        {
            await _filtersValidationService.ValidateAsync(filters, CancellationToken.None);

            var tasks = new List<TaskGet>();
            var dbTasks = (await UnitOfWork.TaskRepository
                    .QueryTasksWithPaginationAndFiltersAsync(
                        page,
                        itemsOnPage,
                        string.IsNullOrEmpty(filters.SearchTerm)
                            ? "%"
                            : filters.SearchTerm.ConvertToSqlLikePattern(),
                        filters.ModuleId,
                        filters.TaskTypeId))
                .ToList();

            foreach (var dbTask in dbTasks)
            {
                var task = Mapper.Map<TaskGet>(dbTask);
                task.Answers = (await UnitOfWork.TaskRepository.QueryAnswersByTaskIdAsync(task.Id)).ToList();
                task.Rights = (await UnitOfWork.TaskRepository.QueryRightsByTaskIdAsync(task.Id)).ToList();
                task.Topics = (await UnitOfWork.TaskRepository.QueryTopicsByTaskIdAsync(task.Id)).ToList();
                task.Tags = (await UnitOfWork.TaskRepository.QueryTagsByTaskIdAsync(task.Id)).ToList();
                tasks.Add(task);
            }

            tasks.Sort((i1, i2) =>
            {
                var i1Time = i1.ChangedAtUtc ?? i1.CreatedAtUtc;
                var i2Time = i2.ChangedAtUtc ?? i2.CreatedAtUtc;
                return filters.SortTypeId == SortType.Newest
                    ? i1Time > i2Time ? 1 : -1
                    : i1Time < i2Time ? 1 : -1;
            });

            return tasks;
        }

        public async Task<IEnumerable<TaskHistoryGet>> GetTaskHistoriesByTaskIdAsync(long taskId)
        {
            return Mapper.Map<IEnumerable<TaskHistoryGet>>(
                await UnitOfWork.TaskRepository.QueryTaskHistoryByTaskIdAsync(taskId));
        }

        public async Task RemoveTaskConflictsAsync(long firstTaskId, long secondTaskId)
        {
            await UnitOfWork.TaskRepository.RemoveTaskConflictAsync(firstTaskId, secondTaskId);
            UnitOfWork.Commit();
        }

        public async Task<IEnumerable<TaskConflictsGet>> GetTaskConflictsAsync()
        {
            var conflicts = new List<TaskConflictsGet>();
            var dbConflicts = (await UnitOfWork.TaskRepository.QueryTaskConflictsAsync()).ToList();

            foreach (var (firstTaskId, secondTaskId) in dbConflicts)
            {
                conflicts.Add(
                    new TaskConflictsGet
                    {
                        FirstTask = await GetAsync(firstTaskId),
                        SecondTask = await GetAsync(secondTaskId),
                    });
            }

            return conflicts;
        }

        public override async Task<IEnumerable<TaskGet>> GetAllAsync()
        {
            var tasks = new List<TaskGet>();
            var dbTasks = await base.GetAllAsync();

            foreach (var dbTask in dbTasks)
            {
                var task = Mapper.Map<TaskGet>(dbTask);
                task.Answers = (await UnitOfWork.TaskRepository.QueryAnswersByTaskIdAsync(task.Id)).ToList();
                task.Rights = (await UnitOfWork.TaskRepository.QueryRightsByTaskIdAsync(task.Id)).ToList();
                task.Topics = (await UnitOfWork.TaskRepository.QueryTopicsByTaskIdAsync(task.Id)).ToList();
                task.Tags = (await UnitOfWork.TaskRepository.QueryTagsByTaskIdAsync(task.Id)).ToList();
                tasks.Add(task);
            }

            return tasks;
        }

        public override async Task<TaskGet> GetAsync(long id)
        {
            var dbTask = await base.GetAsync(id);

            var task = Mapper.Map<TaskGet>(dbTask);
            task.Answers = (await UnitOfWork.TaskRepository.QueryAnswersByTaskIdAsync(task.Id)).ToList();
            task.Rights = (await UnitOfWork.TaskRepository.QueryRightsByTaskIdAsync(task.Id)).ToList();
            task.Topics = (await UnitOfWork.TaskRepository.QueryTopicsByTaskIdAsync(task.Id)).ToList();
            task.Tags = (await UnitOfWork.TaskRepository.QueryTagsByTaskIdAsync(task.Id)).ToList();

            return task;
        }

        public override async Task<TaskGet> CreateAsync(TaskPost data)
        {
            var dbTask = await base.CreateAsync(data);

            await UnitOfWork.TaskRepository.UpdateAnswersByTaskIdAsync(dbTask.Id, data.Answers);
            await UnitOfWork.TaskRepository.UpdateRightsByTaskIdAsync(dbTask.Id, data.Rights);
            await UnitOfWork.TaskRepository.UpdateTopicsByTaskIdAsync(dbTask.Id, data.Topics);
            await UnitOfWork.TaskRepository.UpdateTagsByTaskIdAsync(dbTask.Id, data.Tags);

            UnitOfWork.Commit();
            return dbTask;
        }

        public override async Task<TaskGet> UpdateAsync(long id, TaskPut data)
        {
            var dbTask = await base.UpdateAsync(id, data);

            await UnitOfWork.TaskRepository.UpdateAnswersByTaskIdAsync(dbTask.Id, data.Answers);
            await UnitOfWork.TaskRepository.UpdateRightsByTaskIdAsync(dbTask.Id, data.Rights);
            await UnitOfWork.TaskRepository.UpdateTopicsByTaskIdAsync(dbTask.Id, data.Topics);
            await UnitOfWork.TaskRepository.UpdateTagsByTaskIdAsync(dbTask.Id, data.Tags);

            UnitOfWork.Commit();
            return dbTask;
        }

        public override async Task DeleteAsync(long id)
        {
            await UnitOfWork.TaskRepository.UpdateTopicsByTaskIdAsync(id, new List<string>());
            await UnitOfWork.TaskRepository.UpdateTagsByTaskIdAsync(id, new List<string>());

            await base.DeleteAsync(id);
        }

        protected override async Task<IEnumerable<ArtTask>> GetAllInternalAsync()
        {
            return (await UnitOfWork.TaskRepository.GetAllAsync()).Where(t => t.IsActive);
        }

        protected override async Task<ArtTask> GetInternalAsync(long id)
        {
            return await UnitOfWork.TaskRepository.GetAsync(id);
        }

        protected override async Task CreateInternalAsync(ArtTask entity)
        {
            entity.IsActive = true;
            await UnitOfWork.TaskRepository.CreateAsync(entity);

            var existingTasks = (await UnitOfWork.TaskRepository.GetAllAsync()).Where(t => t.IsActive).ToList();

            var conflictedIds = new List<long>();
            foreach (var existingTask in existingTasks)
            {
                if (entity.Id != existingTask.Id &&
                    entity.Body.LevenshteinSimilarity(existingTask.Body) < 0.1)
                {
                    conflictedIds.Add(existingTask.Id);
                }
            }

            await UnitOfWork.TaskRepository.AddTaskConflictsAsync(entity.Id, conflictedIds);
        }

        protected override async Task UpdateInternalAsync(ArtTask entity)
        {
            await UnitOfWork.TaskRepository.UpdateAsync(entity);

            var existingTasks = (await UnitOfWork.TaskRepository.GetAllAsync()).Where(t => t.IsActive).ToList();

            var conflictedIds = new List<long>();
            foreach (var existingTask in existingTasks)
            {
                if (entity.Id != existingTask.Id &&
                    entity.Body.LevenshteinSimilarity(existingTask.Body) < 0.1)
                {
                    conflictedIds.Add(existingTask.Id);
                }
            }

            await UnitOfWork.TaskRepository.AddTaskConflictsAsync(entity.Id, conflictedIds);
        }

        protected override async Task DeleteInternalAsync(ArtTask entity)
        {
            entity.IsActive = false;

            await UnitOfWork.TaskRepository.UpdateAsync(entity);
            await UnitOfWork.TaskRepository.RemoveTaskConflictsByTaskIdAsync(entity.Id);

            UnitOfWork.Commit();
        }
    }
}
