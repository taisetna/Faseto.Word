using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// Extension methods for the Dna Framework
    /// </summary>
    public static class FrameworkExtensions
    {
        #region Configuration     

        public static FrameworkConstruction Configure(this FrameworkConstruction construction, Action<IConfigurationBuilder> configure = null)
        {
        

            // Create our configuration sources
            var configurationBuilder = new ConfigurationBuilder()
                // Add environment variables
                .AddEnvironmentVariables()
                // Set base path for Json files as the startup location of the application
                .SetBasePath(Directory.GetCurrentDirectory())
                // Add application settings json files
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{construction.Enviroment.Configuration}.json", optional: true, reloadOnChange: true);

            // Let custom configuration sources happen
            configure?.Invoke(configurationBuilder);

            // Inject configuration into services
            var configuration = configurationBuilder.Build();
            construction.Services.AddSingleton<IConfiguration>(configuration);

            // Set the construction Configuration
            construction.Configuration = configuration;                       

            // Chain the construction
            return construction;
        }

        #endregion

        /// <summary>
        /// Injects the default logger into the framework constrction
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddDefaultLogger(this FrameworkConstruction construction)
        {          
            // Add a default logger so that we can get a non-generic ILogger
            construction.Services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Dna"));

            // Chain the construction
            return construction;
        }

        public static FrameworkConstruction UseDefaultServices(this FrameworkConstruction construction)
        {
            // Add exception handler
            construction.AddDefaultExceptionHandler();

            // Add default logging
            construction.AddDefaultLogger();                       

            #region Custom Services and Building

            // Allow custom service injection
            injection?.Invoke(services, configuration);

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();

            #endregion

            // Chain the construction
            return construction;
        }

        public static FrameworkConstruction AddDefaultExceptionHandler(this FrameworkConstruction construction)
        {
            // Bind a static instnace of the BaseExceptionHandler
            construction.Services.AddSingleton<IExceptionHandler>(new BaseExceptionHandler());
             
            // Chain the construction
            return construction;
        }
    }
}
