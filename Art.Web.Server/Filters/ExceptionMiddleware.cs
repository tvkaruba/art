using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Art.Web.Server.Extensions;
using Art.Web.Shared.Models.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Art.Web.Server.Filters
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ValidationException ex:
                    _logger.LogWarning(ex.Message, ex);

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        ex.Errors
                            .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                            .ToList()
                            .ConvertObjectToJson());
                    break;

                case InvalidOperationException ex:
                    _logger.LogWarning(ex.Message, ex);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        new List<ServerError>
                        {
                            new ServerError(ex.Message),
                        }.ConvertObjectToJson());
                    break;

                case SqlException ex:
                    _logger.LogError(ex.Message, ex);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        ex.Errors.Cast<SqlError>()
                            .Select(e => new ServerError(e.Message))
                            .ToList()
                            .ConvertObjectToJson());
                    break;

                case SecurityTokenExpiredException ex:
                    _logger.LogWarning(ex.Message, ex);

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        new List<ServerError>
                        {
                            new ServerError(ex.Message),
                        }.ConvertObjectToJson());
                    break;

                case AuthenticationException ex:
                    _logger.LogWarning(ex.Message, ex);

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        new List<ServerError>
                        {
                            new ServerError(ex.Message),
                        }.ConvertObjectToJson());
                    break;

                case Exception ex:
                    _logger.LogError(ex.Message, ex);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        new List<ServerError>
                        {
                            new ServerError(ex.Message),
                        }.ConvertObjectToJson());
                    break;

                default:
                    _logger.LogError($"Unhandled exception was thrown. Exception context: '{context}'");
                    break;
            }
        }
    }
}
