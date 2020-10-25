using System;
using System.Threading;
using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Variant;

namespace Art.Web.Server.Validators.Variant
{
    /// <inheritdoc cref="IValidationService{VariantPost}" />
    /// <inheritdoc cref="IValidationService{VariantPut}" />
    public class VariantValidationService : ValidationServiceBase, IValidationService<VariantPost>, IValidationService<VariantPut>
    {
        public VariantValidationService(IValidationRules<VariantPost> postRules, IValidationRules<VariantPut> putRules)
        {
            PostRules = postRules ?? throw new ArgumentException(nameof(postRules));
            PutRules = putRules ?? throw new ArgumentException(nameof(postRules));
        }

        private IValidationRules<VariantPost> PostRules { get; }

        private IValidationRules<VariantPut> PutRules { get; }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(VariantPost data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await PostRules.ValidateAsync(data, cancellation));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(VariantPut data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await PutRules.ValidateAsync(data, cancellation));
        }
    }
}
