using Ects.Web.Api.Validators.Infrastructure.Abstractions;
using FluentValidation;

namespace Ects.Web.Api.Validators.Infrastructure
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