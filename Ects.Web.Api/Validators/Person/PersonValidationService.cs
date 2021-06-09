using System;
using System.Threading;
using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Api.Validators.Infrastructure.Abstractions;
using Ects.Web.Shared.Models.Person;

namespace Ects.Web.Api.Validators.Person
{
    /// <inheritdoc cref="IValidationService{T}" />
    /// <inheritdoc cref="IValidationService{PersonPut}" />
    public class PersonValidationService : ValidationServiceBase, IValidationService<PersonPost>,
        IValidationService<PersonPut>
    {
        private IValidationRules<PersonPost> PostRules { get; }

        private IValidationRules<PersonPut> PutRules { get; }

        public PersonValidationService(IValidationRules<PersonPost> postRules, IValidationRules<PersonPut> putRules)
        {
            PostRules = postRules ?? throw new ArgumentException(nameof(postRules));
            PutRules = putRules ?? throw new ArgumentException(nameof(postRules));
        }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task ValidateAsync(
            PersonPost data,
            CancellationToken cancellation = default)
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