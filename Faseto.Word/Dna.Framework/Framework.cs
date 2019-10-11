using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Dna
{
    public static class Framework
    {
        #region Private Memebers
       
        private static IServiceProvider ServiceProvider;
         
        #endregion

        #region Public Properties

        public static IServiceProvider Provider => ServiceProvider;

        public static IConfiguration Configuration => Provider.GetService<IConfiguration>();

        public static ILogger Logger => Provider.GetService<ILogger>();

        public static FrameworkEnvironment Environment => Provider.GetService<FrameworkEnvironment>();

        #endregion

        public static void StartUp(Action<IConfigurationBuilder> configure = null , Action<IServiceCollection, IConfiguration> injection = null)
        {
            #region Initialize

            // Create a new list of dependencies
            var services = new ServiceCollection();

            #endregion

            #region Environment

            // Create environment details
            var environment = new FrameworkEnvironment();

            // Inject environment into services
            services.AddSingleton(environment);

            #endregion

            #region Configuration

            // Create configuration sources
            var configurationBuilder = new ConfigurationBuilder()
                // Add environment variables
                .AddEnvironmentVariables()
                // Set base path for Json files as the startup location of the application
                .SetBasePath(Directory.GetCurrentDirectory())
                // Add application settings json files
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.Configuration}.json", optional: true, reloadOnChange: true);

            // Let custom configuration sources happen
            configure?.Invoke(configurationBuilder);

            // Inject configuration into services
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            #endregion

            #region Logging

            // Add logging as default
            services.AddLogging(options =>
            {
                // Setup loggers from configuration
                options.AddConfiguration(configuration.GetSection("Logging"));

                // Add console logger
                options.AddConsole();

                // Add debug logger
                options.AddDebug();

                // Add file logger
                options.AddFile("log.txt");
                //options.AddFile("log2.txt");
            });

            // Add default logger
            services.AddDefaultLogger();

            #endregion

            #region Custom Services and Building 

            // Allow custom service injection
            injection?.Invoke(services, configuration);

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();

            #endregion

            // Log the start complete
            Logger.LogCriticalSource($"Dna Framework started in {environment.Configuration}...");
        }
    }

    public class Test { }
}
