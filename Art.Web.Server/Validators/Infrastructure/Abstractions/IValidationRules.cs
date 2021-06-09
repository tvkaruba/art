using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Art.Web.Server.Validators.Infrastructure.Abstractions
{
    /// <summary>
    /// Provide a way for object to be validated.
    /// </summary>
    /// <typeparam name="T">Type of object to be validated</typeparam>
    public interface IValidationRules<in T>
        where T : class
    {
        /// <summary>
        /// Perform object validation asynchronously.
        /// </summary>
        /// <param name="data">Object to be validated</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<ValidationResult> ValidateAsync(T data, CancellationToken cancellation);
    }
}