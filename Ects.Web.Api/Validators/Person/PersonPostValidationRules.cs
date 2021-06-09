using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Shared.Models.Person;
using FluentValidation;

namespace Ects.Web.Api.Validators.Person
{
    /// <inheritdoc />
    public class PersonPostValidationRules : ValidationRulesBase<PersonPost>
    {
        public PersonPostValidationRules()
        {
            RuleFor(data => data)
                .NotNull();

            RuleFor(data => data.Email)
                .NotNull()
                .NotEmpty()
                .Matches(".*@.*");

            RuleFor(data => data.Password)
                .NotNull()
                .NotEmpty();

            RuleFor(data => data.PersonRoleId)
                .IsInEnum();
        }
    }
}