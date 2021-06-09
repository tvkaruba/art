using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using Art.Persistence.Infrastructure;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Web.Server.Filters;
using Art.Web.Server.Services;
using Art.Web.Server.Services.Abstractions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Server.Validators.Person;
using Art.Web.Server.Validators.Task;
using Art.Web.Server.Validators.Variant;
using Art.Web.Shared.Models.Person;
using Art.Web.Shared.Models.Task;
using Art.Web.Shared.Models.Variant;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Art.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson()
                .AddMvcOptions(options =>
                {
                    options.CacheProfiles.Add(
                        CachingProfiles.Caching3600,
                        new CacheProfile
                        {
                            Duration = 3600,
                        });

                    options.CacheProfiles.Add(
                        CachingProfiles.NoCaching,
                        new CacheProfile
                        {
                            Location = ResponseCacheLocation.None,
                            NoStore = true,
                        });
                });

            // Configure swagger.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "ART API", Version = "v1"});

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Art.Web.Server.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Art.Web.Shared.xml"));

                options.EnableAnnotations();

                options.SchemaFilter<ApiSchemaFilter>();
                options.OperationFilter<ApiOperationFilter>();

                options.AddSecurityDefinition(
                    name: "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Please insert JWT with Bearer into field",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
            });

            // Configure CORS.
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "login",
                    builder => builder
                        .WithOrigins(Configuration.GetValue<string>("AllowedOrigins").Trim()
                            .Split(new[] {';', ','}, StringSplitOptions.RemoveEmptyEntries))
                        .WithHeaders(
                            HeaderNames.Accept,
                            HeaderNames.Origin,
                            HeaderNames.ContentType)
                        .WithMethods(
                            HttpMethod.Get.Method,
                            HttpMethod.Post.Method,
                            HttpMethod.Put.Method,
                            HttpMethod.Delete.Method,
                            HttpMethod.Options.Method)
                        .AllowCredentials()
                );

                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(Configuration.GetValue<string>("AllowedOrigins").Trim()
                            .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
                        .WithHeaders(
                            HeaderNames.Accept,
                            HeaderNames.Origin,
                            HeaderNames.ContentType,
                            HeaderNames.Authorization)
                        .WithMethods(
                            HttpMethod.Get.Method,
                            HttpMethod.Post.Method,
                            HttpMethod.Put.Method,
                            HttpMethod.Delete.Method,
                            HttpMethod.Options.Method)
                        .AllowCredentials();
                });
            });

            // Configure response compression.
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                });
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<UnitOfWorkConfiguration>()
                .As<IUnitOfWorkConfiguration>()
                .WithParameter(
                    parameterName: "connectionString",
                    parameterValue: Configuration.GetConnectionString("ART"))
                .SingleInstance();

            var mapperConfig = new MapperConfiguration(c => c.AddMaps(Assembly.GetExecutingAssembly()));
            builder
                .RegisterInstance(mapperConfig)
                .As<MapperConfiguration>()
                .SingleInstance();
            builder
                .Register(_ => new Mapper(mapperConfig))
                .As<IMapper>();

            builder
                .RegisterType<JwtTokenService>()
                .As<IJwtTokenService>()
                .InstancePerDependency();

            builder
                .RegisterType<PersonService>()
                .As<IPersonService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<PersonValidationService>()
                .As<IValidationService<PersonPost>>()
                .SingleInstance();
            builder
                .RegisterType<PersonValidationService>()
                .As<IValidationService<PersonPut>>()
                .SingleInstance();
            builder
                .RegisterType<PersonPostValidationRules>()
                .As<IValidationRules<PersonPost>>()
                .SingleInstance();
            builder
                .RegisterType<PersonPutValidationRules>()
                .As<IValidationRules<PersonPut>>()
                .SingleInstance();

            builder
                .RegisterType<TaskService>()
                .As<ITaskService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<TaskValidationService>()
                .As<IValidationService<TaskPost>>()
                .SingleInstance();
            builder
                .RegisterType<TaskValidationService>()
                .As<IValidationService<TaskPut>>()
                .SingleInstance();
            builder
                .RegisterType<TaskValidationService>()
                .As<IValidationService<TaskFilters>>()
                .SingleInstance();
            builder
                .RegisterType<TaskPostValidationRules>()
                .As<IValidationRules<TaskPost>>()
                .SingleInstance();
            builder
                .RegisterType<TaskPutValidationRules>()
                .As<IValidationRules<TaskPut>>()
                .SingleInstance();
            builder
                .RegisterType<TaskFiltersValidationRules>()
                .As<IValidationRules<TaskFilters>>()
                .SingleInstance();

            builder
                .RegisterType<VariantService>()
                .As<IVariantService>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<VariantValidationService>()
                .As<IValidationService<VariantPost>>()
                .SingleInstance();
            builder
                .RegisterType<VariantValidationService>()
                .As<IValidationService<VariantPut>>()
                .SingleInstance();
            builder
                .RegisterType<VariantPostValidationRules>()
                .As<IValidationRules<VariantPost>>()
                .SingleInstance();
            builder
                .RegisterType<VariantPutValidationRules>()
                .As<IValidationRules<VariantPut>>()
                .SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime appLifetime)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionMiddleware>();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Art.Web.Server V1");
                options.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();
            app.UseMiddleware<JwtAuthenticationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            appLifetime.ApplicationStopped.Register(() => AutofacContainer.Dispose());
        }
    }
}
