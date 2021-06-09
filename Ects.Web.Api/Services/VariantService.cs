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
using Ects.Web.Shared.Models.Variant;

namespace Ects.Web.Api.Services
{
    public class VariantService :
        ValidatableCrudWithAuditServiceBase<Question, long, VariantPost, VariantPut, VariantGet>, IVariantService
    {
        public VariantService(
            IUnitOfWork uow,
            IMapper mapper,
            IValidationService<VariantPost> createValidationService,
            IValidationService<VariantPut> updateValidationService)
            : base(uow, mapper, createValidationService, updateValidationService) { }

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

        public Task<IEnumerable<TaskGet>> GetTasksByVariantIdAsync(long variantId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VariantHistoryGet>> GetVariantHistoryAsync(long variantId)
        {
            throw new NotImplementedException();
        }

        public Task<VariantExportGet> GetVariantForExportByVariantId(long variantId)
        {
            throw new NotImplementedException();
        }
    }
}