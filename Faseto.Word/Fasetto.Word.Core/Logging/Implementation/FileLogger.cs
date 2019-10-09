using System;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Public Properties

        public string FilePath { get; set; }

        public bool LogTime { get; set; } = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileLogger(string path)
        {
            // Set the file property
            FilePath = path;
        }

        #endregion

        #region Logger Methods

        public void Log(string message, LogLevel level)
        {
            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd mm:hh:ss");

            // Prepend the time to the log if desired
            var timeLogString = LogTime ? $"[{currentTime}]" : "";
            
            // Write the message to log file
            IoC.File.WriteAllTextToFileAsync($"{currentTime}{message}{Environment.NewLine}", FilePath, append: true);
        }
        #endregion
    }
}
