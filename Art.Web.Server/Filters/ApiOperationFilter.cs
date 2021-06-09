using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Art.Web.Shared.Models.Errors;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Art.Web.Server.Filters
{
    public class ApiOperationFilter : IOperationFilter
    {
        private readonly Dictionary<HttpStatusCode, OpenApiResponse> _commonResponseCodes = new Dictionary<HttpStatusCode, OpenApiResponse>()
        {
            [HttpStatusCode.Unauthorized] = new OpenApiResponse
            {
                Description = "Operation prohibited due to system roles policy and requesting user's role",
            },
            [HttpStatusCode.InternalServerError] = new OpenApiResponse
            {
                Description = "Generic server-side error",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "array",
                            Reference = new OpenApiReference
                            {
                                Id = $"components/schemas/{nameof(ServerError)}",
                                ExternalResource = "swagger.json",
                            },
                        },
                    },
                },
            },
        };

        private readonly Dictionary<HttpStatusCode, OpenApiResponse> _updateResponseCodes = new Dictionary<HttpStatusCode, OpenApiResponse>
        {
            [HttpStatusCode.NoContent] = new OpenApiResponse
            {
                Description = "Resource modified successfully",
            },
            [HttpStatusCode.NotFound] = new OpenApiResponse
            {
                Description = "Requesting resource is not found",
            },
            [HttpStatusCode.BadRequest] = new OpenApiResponse
            {
                Description = "Input data was in invalid format",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "array",
                            Reference = new OpenApiReference
                            {
                                Id = $"components/schemas/{nameof(ValidationError)}",
                                ExternalResource = "swagger.json",
                            },
                        },
                    },
                },
            },
        };

        private readonly Dictionary<HttpStatusCode, OpenApiResponse> _createResponseCodes = new Dictionary<HttpStatusCode, OpenApiResponse>
        {
            [HttpStatusCode.Created] = new OpenApiResponse
            {
                Description = "Resource created successfully",
                Headers = new Dictionary<string, OpenApiHeader>
                {
                    ["Location"] = new OpenApiHeader
                    {
                        Description = "URI of created resource",
                    },
                },
            },
            [HttpStatusCode.BadRequest] = new OpenApiResponse
            {
                Description = "Input data was in invalid format",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "array",
                            Reference = new OpenApiReference
                            {
                                Id = $"components/schemas/{nameof(ValidationError)}",
                                ExternalResource = "swagger.json",
                            },
                        },
                    },
                },
            },
            [HttpStatusCode.Conflict] = new OpenApiResponse
            {
                Description = "Request generate conflict situation",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "array",
                            Reference = new OpenApiReference
                            {
                                Id = $"components/schemas/{nameof(ServerError)}",
                                ExternalResource = "swagger.json",
                            },
                        },
                    },
                },
            },
        };

        private readonly Dictionary<HttpStatusCode, OpenApiResponse> _deleteResponseCodes = new Dictionary<HttpStatusCode, OpenApiResponse>
        {
            [HttpStatusCode.NoContent] = new OpenApiResponse
            {
                Description = "Resource deleted successfully",
            },
            [HttpStatusCode.NotFound] = new OpenApiResponse
            {
                Description = "Requesting resource is not found",
            },
        };

        private readonly Dictionary<HttpStatusCode, OpenApiResponse> _readResponseCodes = new Dictionary<HttpStatusCode, OpenApiResponse>
        {
            [HttpStatusCode.OK] = new OpenApiResponse
            {
                Description = "Request resulted in success",
            },
            [HttpStatusCode.NotFound] = new OpenApiResponse
            {
                Description = "Requesting resource is not found",
            },
        };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            static void ApplyDocumentation(Dictionary<HttpStatusCode, OpenApiResponse> codeMap, OpenApiOperation op)
            {
                foreach (var code in codeMap.Keys)
                {
                    var codeNum = ((int)code).ToString();

                    if (!op.Responses.ContainsKey(codeNum))
                    {
                        op.Responses.Add(codeNum, codeMap[code]);
                    }
                }
            }

            operation.Responses ??= new OpenApiResponses();
            ApplyDocumentation(_commonResponseCodes, operation);

            switch (context.ApiDescription.HttpMethod)
            {
                case { } method when method == HttpMethod.Post.Method:
                    ApplyDocumentation(_createResponseCodes, operation);
                    break;

                case { } method when method == HttpMethod.Put.Method:
                    ApplyDocumentation(_updateResponseCodes, operation);
                    break;

                case { } method when method == HttpMethod.Delete.Method:
                    ApplyDocumentation(_deleteResponseCodes, operation);
                    break;

                case { } method when method == HttpMethod.Get.Method:
                    ApplyDocumentation(_readResponseCodes, operation);
                    break;

                default:
                    throw new InvalidOperationException("Unexpected http method in swagger gen.");
            }
        }
    }

}
