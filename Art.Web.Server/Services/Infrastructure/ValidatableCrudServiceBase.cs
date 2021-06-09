using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Web.Server.Services.Infrastructure.Abstractions;
using Art.Web.Server.Validators.Infrastructure.Abstractions;

namespace Art.Web.Server.Services.Infrastructure
{
    public abstract class ValidatableCrudServiceBase<TEntity, TKey, TPost, TPut, TGet> : CrudServiceBase<TEntity, TKey, TPost, TPut, TGet>, IValidatableCrudService<TKey, TPost, TPut, TGet>
        where TEntity : IEntity<TKey>
        where TPost : class
        where TPut : class
    {
        protected readonly IValidationService<TPost> CreateValidationService;

        protected readonly IValidationService<TPut> UpdateValidationService;

        protected ValidatableCrudServiceBase(
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