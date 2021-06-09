using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ects.Web.Api.Extensions
{
    public static class JsonExtensions
    {
        public static string ConvertObjectToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}