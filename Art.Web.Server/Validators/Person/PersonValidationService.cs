using System;
using System.Threading;
using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Person;

namespace Art.Web.Server.Validators.Person
{
    /// <inheritdoc cref="IValidationService{PersonPost}" />
    /// <inheritdoc cref="IValidationService{PersonPut}" />
    public class PersonValidationService : ValidationServiceBase, IValidationService<PersonPost>, IValidationService<PersonPut>
    {
        public PersonValidationService(IValidationRules<PersonPost> postRules, IValidationRules<PersonPut> putRules)
        {
            PostRules = postRules ?? throw new ArgumentException(nameof(postRules));
            PutRules = putRules ?? throw new ArgumentException(nameof(postRules));
        }

        private IValidationRules<PersonPost> PostRules { get; }

        private IValidationRules<PersonPut> PutRules { get; }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(PersonPost data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await PostRules.ValidateAsync(data, cancellation));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(PersonPut data, CancellationToken cancellation = default)
        {
            CheckAndThrowValidationException(await PutRules.ValidateAsync(data, cancellation));
        }
    }
}