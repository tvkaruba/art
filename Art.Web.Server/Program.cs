using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Art.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                    config
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile(
                            path: "logsettings.json",
                            optional: false)
                        .AddJsonFile(
                            path: "connectionsettings.json",
                            optional: false))
                .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
        }
    }
}
