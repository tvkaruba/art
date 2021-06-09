using System;
using System.Threading;
using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Api.Validators.Infrastructure.Abstractions;
using Ects.Web.Shared.Models.Variant;

namespace Ects.Web.Api.Validators.Variant
{
    /// <inheritdoc cref="IValidationService{T}" />
    /// <inheritdoc cref="IValidationService{VariantPut}" />
    public class VariantValidationService : ValidationServiceBase, IValidationService<VariantPost>,
        IValidationService<VariantPut>
    {
        private IValidationRules<VariantPost> PostRules { get; }

        private IValidationRules<VariantPut> PutRules { get; }

        public VariantValidationService(IValidationRules<VariantPost> postRules, IValidationRules<VariantPut> putRules)
        {
            PostRules = postRules ?? throw new ArgumentException(nameof(postRules));
            PutRules = putRules ?? throw new ArgumentException(nameof(postRules));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(
            VariantPost data,
            CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await PostRules.ValidateAsync(data, cancellation));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(
            VariantPut data,
            CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await PutRules.ValidateAsync(data, cancellation));
        }
    }
}