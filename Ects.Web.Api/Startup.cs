using System;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Ects.Persistence;
using Ects.Persistence.Abstractions;
using Ects.Web.Api.Filters;
using Ects.Web.Api.Services;
using Ects.Web.Api.Services.Abstractions;
using Ects.Web.Api.Validators.Infrastructure.Abstractions;
using Ects.Web.Api.Validators.Person;
using Ects.Web.Api.Validators.Task;
using Ects.Web.Api.Validators.Variant;
using Ects.Web.Shared.Models.Person;
using Ects.Web.Shared.Models.Task;
using Ects.Web.Shared.Models.Variant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace Ects.Web.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services
                .AddControllers()
                .AddMvcOptions(options =>
                {
                    options.CacheProfiles.Add(
                        CachingProfiles.Caching3600,
                        new CacheProfile
                        {
                            Duration = 3600
                        });

                    options.CacheProfiles.Add(
                        CachingProfiles.NoCaching,
                        new CacheProfile
                        {
                            Location = ResponseCacheLocation.None,
                            NoStore = true
                        });
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECTS API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "login",
                    builder => builder
                        .WithOrigins(Configuration.GetValue<string>("AllowedOrigins").Trim()
                            .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries))
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

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet
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
                    "connectionString",
                    Configuration.GetConnectionString("ECTS"))
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
                .RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerDependency();
            builder
                .RegisterType<CommentService>()
                .As<ICommentService>()
                .InstancePerDependency();
            builder
                .RegisterType<ExamService>()
                .As<IExamService>()
                .InstancePerDependency();
            builder
                .RegisterType<ExamAnswerService>()
                .As<IExamAnswerService>()
                .InstancePerDependency();
            builder
                .RegisterType<ExamParticipantService>()
                .As<IExamParticipantService>()
                .InstancePerDependency();
            builder
                .RegisterType<ImageService>()
                .As<IImageService>()
                .InstancePerDependency();
            builder
                .RegisterType<NamespaceService>()
                .As<INamespaceService>()
                .InstancePerDependency();
            builder
                .RegisterType<QuestionConflictService>()
                .As<IQuestionConflictService>()
                .InstancePerDependency();
            builder
                .RegisterType<QuestionHistoryService>()
                .As<IQuestionHistoryService>()
                .InstancePerDependency();
            builder
                .RegisterType<QuestionService>()
                .As<IQuestionService>()
                .InstancePerDependency();
            builder
                .RegisterType<QuestionTypeService>()
                .As<IQuestionTypeService>()
                .InstancePerDependency();
            builder
                .RegisterType<TagService>()
                .As<ITagService>()
                .InstancePerDependency();
            builder
                .RegisterType<TestService>()
                .As<ITestService>()
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ects.Web.Api v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            appLifetime.ApplicationStopped.Register(() => AutofacContainer.Dispose());
        }
    }
}