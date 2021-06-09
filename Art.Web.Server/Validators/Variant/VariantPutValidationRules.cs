using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Shared.Models.Variant;
using FluentValidation;

namespace Art.Web.Server.Validators.Variant
{
    public class VariantPutValidationRules : ValidationRulesBase<VariantPut>
    {
        public VariantPutValidationRules()
        {
            RuleFor(data => data)
                .NotNull();

            RuleFor(data => data.ModuleId)
                .IsInEnum();

            RuleFor(data => data.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
