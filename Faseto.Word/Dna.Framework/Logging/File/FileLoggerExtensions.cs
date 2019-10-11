using Microsoft.Extensions.Logging;

namespace Dna
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path, FileLoggerConfiguration configuration = null)
        {
            // Create default configuration if not provided
            if (configuration == null)
                configuration = new FileLoggerConfiguration();

            // Add file log provider to builder
            builder.AddProvider(new FileLoggerProvider(path, configuration));

            // Return the builder
            return builder;
        }
    }
}
