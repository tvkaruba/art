using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Shared.Models.Variant;
using FluentValidation;

namespace Art.Web.Server.Validators.Variant
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
