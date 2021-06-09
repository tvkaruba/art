using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ects.Persistence.Abstractions;
using Ects.Persistence.Models.Abstractions;
using Ects.Web.Api.Services.Infrastructure.Abstractions;
using Ects.Web.Api.Validators.Infrastructure.Abstractions;

namespace Ects.Web.Api.Services.Infrastructure
{
    public abstract class ValidatableCrudWithAuditServiceBase<TEntity, TKey, TPost, TPut, TGet> :
        CrudWithAuditServiceBase<TEntity, TKey, TPost, TPut, TGet>,
        IValidatableCrudWithAuditService<TKey, TPost, TPut, TGet>
        where TEntity : IEntity<TKey>, IAuditable
        where TPost : class
        where TPut : class
    {
        protected readonly IValidationService<TPost> CreateValidationService;

        protected readonly IValidationService<TPut> UpdateValidationService;

        protected ValidatableCrudWithAuditServiceBase(
            IUnitOfWork uow,
            IMapper mapper,
            IValidationService<TPost> createValidationService,
            IValidationService<TPut> updateValidationService)
            : base(uow, mapper)
        {
            CreateValidationService = createValidationService;
            UpdateValidationService = updateValidationService;
        }

        public override async Task<TGet> CreateAsync(TPost data)
        {
            await CreateValidationService.ValidateAsync(data, CancellationToken.None);

            return await base.CreateAsync(data);
        }

        public override async Task<TGet> UpdateAsync(TKey id, TPut data)
        {
            await UpdateValidationService.ValidateAsync(data, CancellationToken.None);

            return await base.UpdateAsync(id, data);
        }
    }
}