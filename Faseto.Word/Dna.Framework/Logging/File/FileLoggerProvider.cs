using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        #region Protected Memebers

        protected string mFilePath;

        protected readonly FileLoggerConfiguration mConfiguration;

        protected readonly ConcurrentDictionary<string, FileLogger> mLoggers = new ConcurrentDictionary<string, FileLogger>();
        #endregion

        #region Constructor

        public FileLoggerProvider(string path, FileLoggerConfiguration configuration)
        {
            // Set the configuration
            mConfiguration = configuration;

            // Set the path
            mFilePath = path;
        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName)
        {
            // Get or create the logger for this catergory
            return mLoggers.GetOrAdd(categoryName, name => new FileLogger(name, mFilePath, mConfiguration));
        }

        public void Dispose()
        {
            // Clear the list of Loggers
            mLoggers.Clear();
        }

        #endregion
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileLoggerProvider()
        {

        }

        #endregion
    }
}
