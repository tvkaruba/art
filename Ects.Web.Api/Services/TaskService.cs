using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ects.Persistence.Abstractions;
using Ects.Persistence.Models;
using Ects.Web.Api.Services.Abstractions;
using Ects.Web.Api.Services.Infrastructure;
using Ects.Web.Api.Validators.Infrastructure.Abstractions;
using Ects.Web.Shared.Models.Task;

namespace Ects.Web.Api.Services
{
    public class TaskService : ValidatableCrudWithAuditServiceBase<Question, long, TaskPost, TaskPut, TaskGet>,
        ITaskService
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

        protected override Task<IEnumerable<Question>> GetAllInternalAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task<Question> GetInternalAsync(long id)
        {
            throw new NotImplementedException();
        }

        protected override Task CreateInternalAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateInternalAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteInternalAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetTasksCountWithFiltersAsync(TaskFilters filters)
        {
            throw new NotImplementedException();
        }

        public Task<TaskTopicsGet> GetAllTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskTagsGet> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskGet>> GetTasksWithPaginationAndFiltersAsync(
            int page,
            int itemsOnPage,
            TaskFilters filters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskHistoryGet>> GetTaskHistoriesByTaskIdAsync(long taskId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTaskConflictsAsync(long firstTaskId, long secondTaskId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskConflictsGet>> GetTaskConflictsAsync()
        {
            throw new NotImplementedException();
        }
    }
}