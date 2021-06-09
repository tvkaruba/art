using System.Threading;
using System.Threading.Tasks;

namespace Art.Web.Server.Validators.Infrastructure.Abstractions
{
    /// <summary>
    /// Provide a way to validate some objects of <typeparam name="T">T</typeparam>
    /// </summary>
    public interface IValidationService<in T>
        where T : class
    {
        /// <summary>
        /// Perform asynchronous validation of input data.
        /// </summary>
        /// <param name="data">Validatable object.</param>
        /// <param name="cancellation"></param>
        System.Threading.Tasks.Task ValidateAsync(T data, CancellationToken cancellation);
    }
}
