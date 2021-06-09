using Art.Web.Server.Validators.Infrastructure.Abstractions;
using FluentValidation;

namespace Art.Web.Server.Validators.Infrastructure
{
    public class ValidationRulesBase<T> : AbstractValidator<T>, IValidationRules<T>
        where T : class
    {
        public ValidationRulesBase()
        {
            CascadeMode = CascadeMode.Stop;
        }
    }
}