using System;
using Art.Persistence.Entities;
using Art.Web.Server.Services.Abstractions;
using Art.Web.Server.Services.Infrastructure;
using Art.Web.Shared.Models.Variant;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Task;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Art.Web.Server.Services
{
    public class VariantService : ValidatableCrudWithAuditServiceBase<Variant, long, VariantPost, VariantPut, VariantGet>, IVariantService
    {
        public VariantService(
            IUnitOfWork uow,
            IMapper mapper,
            IValidationService<VariantPost> createValidationService,
            IValidationService<VariantPut> updateValidationService)
            : base(uow, mapper, createValidationService, updateValidationService)
        {
        }

        public async Task<IEnumerable<TaskGet>> GetTasksByVariantIdAsync(long variantId)
        {
            var tasks = new List<TaskGet>();
            var dbTasks = (await UnitOfWork.VariantRepository.QueryTasksByVariantIdAsync(variantId)).ToList();

            foreach (var dbTask in dbTasks)
            {
                var task = Mapper.Map<TaskGet>(dbTask);
                task.Answers = (await UnitOfWork.TaskRepository.QueryAnswersByTaskIdAsync(task.Id)).ToList();
                task.Rights = (await UnitOfWork.TaskRepository.QueryAnswersByTaskIdAsync(task.Id)).ToList();
                task.Topics = (await UnitOfWork.TaskRepository.QueryTopicsByTaskIdAsync(task.Id)).ToList();
                task.Tags = (await UnitOfWork.TaskRepository.QueryTagsByTaskIdAsync(task.Id)).ToList();
                tasks.Add(task);
            }

            return tasks;
        }

        public Task<IEnumerable<VariantHistoryGet>> GetVariantHistoryAsync(long variantId)
        {
            throw new ArgumentException();
            //return Mapper.Map<IEnumerable<VariantHistoryGet>>(
            //    await UnitOfWork.VariantRepository.QueryVariantChangeLogByVariantIdAsync(variantId));
        }

        public async Task<VariantExportGet> GetVariantForExportByVariantId(long variantId)
        {
            var variantGet = await UnitOfWork.VariantRepository.GetAsync(variantId);
            var variantTasks = await GetTasksByVariantIdAsync(variantId);

            return new VariantExportGet
            {
                Id = variantGet.Id,
                ModuleId = variantGet.ModuleId,
                Name = variantGet.Name,
                Tasks = variantTasks,
            };
        }

        public override async Task<VariantGet> CreateAsync(VariantPost data)
        {
            var dbVariant = await base.CreateAsync(data);

            await UnitOfWork.VariantRepository.UpdateTasksByVariantIdAsync(dbVariant.Id, data.TaskIds);

            UnitOfWork.Commit();
            return dbVariant;
        }

        public override async Task<VariantGet> UpdateAsync(long id, VariantPut data)
        {
            var dbVariant = await base.UpdateAsync(id, data);

            await UnitOfWork.VariantRepository.UpdateTasksByVariantIdAsync(dbVariant.Id, data.TaskIds);

            UnitOfWork.Commit();
            return dbVariant;
        }

        protected override async Task CreateInternalAsync(Variant entity)
        {
            entity.IsActive = true;
            await UnitOfWork.VariantRepository.CreateAsync(entity);
        }

        protected override async Task DeleteInternalAsync(Variant entity)
        {
            entity.IsActive = false;
            await UnitOfWork.VariantRepository.UpdateAsync(entity);
            UnitOfWork.Commit();
        }

        protected override async Task<IEnumerable<Variant>> GetAllInternalAsync()
        {
            return (await UnitOfWork.VariantRepository.GetAllAsync()).Where(v => v.IsActive);
        }

        protected override async Task<Variant> GetInternalAsync(long id)
        {
            return await UnitOfWork.VariantRepository.GetAsync(id);
        }

        protected override async Task UpdateInternalAsync(Variant entity)
        {
            await UnitOfWork.VariantRepository.UpdateAsync(entity);
        }
    }
}
