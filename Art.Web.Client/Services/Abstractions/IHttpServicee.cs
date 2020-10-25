using System.Collections.Generic;
using System.Threading.Tasks;

namespace Art.Web.Client.Services.Abstractions
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri, IDictionary<string, object> filters = null);

        Task<T> Post<T>(string uri, object value);

        Task Put(string uri, object value);

        Task Delete(string uri);
    }
}
