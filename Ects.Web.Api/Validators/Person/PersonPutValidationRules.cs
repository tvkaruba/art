using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Shared.Models.Person;
using FluentValidation;

namespace Ects.Web.Api.Validators.Person
{
    public class PersonPutValidationRules : ValidationRulesBase<PersonPut>
    {
        public PersonPutValidationRules()
        {
            RuleFor(data => data)
                .NotNull();
        }
    }
}