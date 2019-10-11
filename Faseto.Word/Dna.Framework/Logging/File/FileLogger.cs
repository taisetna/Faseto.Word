using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Properties

        protected static ConcurrentDictionary<string,object> FileLocks = new ConcurrentDictionary<string, object>();
        #endregion

        #region Protected Memebrs

        protected string mCategoryName;

        protected string mFilePath;

        protected FileLoggerConfiguration mConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileLogger(string categoryName, string filePath, FileLoggerConfiguration configuration)
        {
            // Get absolute path
            filePath = Path.GetFullPath(filePath);
             
            // Set members
            mCategoryName = categoryName;
            mFilePath = filePath;
            mConfiguration = configuration;
        }

        #endregion


        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel >= mConfiguration.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // If we should not log...
            if (!IsEnabled(logLevel))
                // Return
                return;

            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend the time to the log if desired
            var timeLogString = mConfiguration.LogTime ? $"[{ currentTime}] " : "";

            // Get the formatted message string
            var message = formatter(state, exception);

            // Write the message
            var output = $"{timeLogString}{message}{Environment.NewLine}";

            // Normalize the path
            // TODO: Make use of configuration base path
            var normalizedPath = mFilePath.ToUpper();

            // Get the file lock based on absolute path
            var fileLock = FileLocks.GetOrAdd(normalizedPath, path => new object());

            // Lock the file
            lock(fileLock)
            {
                // Write the message to the file
                File.AppendAllText(mFilePath, message);
            }
        }
    }
}
