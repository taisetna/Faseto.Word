using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public class FileLoggerConfiguration
    {
        #region Public Properties

        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        public string FilePath {get; set;}

        public bool LogTime { get; set; } = true;

        #endregion
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileLoggerConfiguration()
        {

        }
        #endregion
    }
}
