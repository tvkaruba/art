using FluentValidation;
using FluentValidation.Results;

namespace Ects.Web.Api.Validators.Infrastructure
{
    /// <summary>
    /// Base class for validation services that reports errors in the same manner.
    /// </summary>
    public abstract class ValidationServiceBase
    {
        /// <summary>
        /// Utility method which checks if result is valid and raises
        /// <exception cref="ValidationException">ValidationException</exception> if it is not.
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        /// <param name="result"></param>
        public void CheckAndThrowValidationException(ValidationResult result)
        {
            if (!result.IsValid) throw new ValidationException(result.Errors);
        }
    }
}