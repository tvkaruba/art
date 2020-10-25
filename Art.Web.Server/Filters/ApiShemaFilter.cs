using Art.Web.Shared.Models.Errors;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Art.Web.Server.Filters
{
    public class ApiSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(ValidationError), context.SchemaRepository);
            context.SchemaGenerator.GenerateSchema(typeof(ServerError), context.SchemaRepository);
        }
    }
}
