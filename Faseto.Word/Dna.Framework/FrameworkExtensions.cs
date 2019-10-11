using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dna
{
    public static class FrameworkExtensions
    {
        public static IServiceCollection AddDefaultLogger(this IServiceCollection services)
        {
            // Add a default logger
            services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Dna"));

            // Return the services
            return services;
        }
    }
}
