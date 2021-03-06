using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

#pragma warning disable 1591

namespace DaprMicroserviceTemplate1
{
    public static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var envstring = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            XmlConfigurator.Configure(logRepo, new FileInfo($"log4net.{envstring}.config"));
            log.Info($"Microservice DaprMicroserviceTemplate1 starting");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();

                });
    }
}
