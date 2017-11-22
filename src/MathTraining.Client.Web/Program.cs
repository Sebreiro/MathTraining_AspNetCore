using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using Autofac;
using MathTraining.Client.Web.Initialization;


namespace MathTraining.Client.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                var host = BuildWebHost(args);

                //SEED Database
                DatabaseInitializer.Initialize(host.Services).Wait();

                host.Run();

            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
            }
        }

        private static IConfigurationRoot Config(string[] args) => new ConfigurationBuilder()
            .AddJsonFile("Config/hosting.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .CaptureStartupErrors(true)
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                //.UseApplicationInsights()
                .UseConfiguration(Config(args))
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                .ConfigureServices(service => service.AddAutofac())
                .UseNLog()
                .UseStartup<Startup>()
                .Build();
        
    }
}
