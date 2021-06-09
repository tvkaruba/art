using System;
using System.Threading;
using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Task;

namespace Art.Web.Server.Validators.Task
{
    /// <inheritdoc cref="IValidationService{TaskPost}" />
    /// <inheritdoc cref="IValidationService{TaskPut}" />
    /// <inheritdoc cref="IValidationService{TaskFilters}" />
    public class TaskValidationService
        : ValidationServiceBase
        , IValidationService<TaskPost>
        , IValidationService<TaskPut>
        , IValidationService<TaskFilters>
    {
        private readonly IValidationRules<TaskPost> _postRules;

        private readonly IValidationRules<TaskPut> _putRules;

        private readonly IValidationRules<TaskFilters> _filtersRules;

        public TaskValidationService(
            IValidationRules<TaskPost> postRules,
            IValidationRules<TaskPut> putRules,
            IValidationRules<TaskFilters> filtersRules)
        {
            _postRules = postRules ?? throw new ArgumentException(nameof(postRules));
            _putRules = putRules ?? throw new ArgumentException(nameof(postRules));
            _filtersRules = filtersRules ?? throw new ArgumentException(nameof(filtersRules));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(TaskPost data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await _postRules.ValidateAsync(data, cancellation));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(TaskPut data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await _putRules.ValidateAsync(data, cancellation));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(TaskFilters data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await _filtersRules.ValidateAsync(data, cancellation));
        }
    }
}