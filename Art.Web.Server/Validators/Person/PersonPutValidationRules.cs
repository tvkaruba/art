using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Shared.Models.Person;
using FluentValidation;

namespace Art.Web.Server.Validators.Person
{
    public class PersonPutValidationRules : ValidationRulesBase<PersonPut>
    {
        public PersonPutValidationRules()
        {
            RuleFor(data => data)
                .NotNull();

            RuleFor(data => data.PersonRoleId)
                .IsInEnum()
                .When(data => data.PersonRoleId.HasValue);

            RuleFor(data => data.Email)
                .Matches(".*@.*")
                .When(data => data.Email != null);
        }
    }
}