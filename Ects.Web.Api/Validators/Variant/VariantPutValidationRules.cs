using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Shared.Models.Variant;
using FluentValidation;

namespace Ects.Web.Api.Validators.Variant
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