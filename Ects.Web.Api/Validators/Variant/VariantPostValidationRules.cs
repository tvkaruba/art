using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Shared.Models.Variant;
using FluentValidation;

namespace Ects.Web.Api.Validators.Variant
{
    /// <inheritdoc />
    public class VariantPostValidationRules : ValidationRulesBase<VariantPost>
    {
        public VariantPostValidationRules()
        {
            RuleFor(data => data)
                .NotNull();

            RuleFor(data => data.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(data => data.ModuleId)
                .IsInEnum();
        }
    }
}