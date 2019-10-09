using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Fasetto.Word.Core
{
    public class BaseLogFactory : ILogFactory
    {
        #region Protected Methods

        protected List<ILogger> mLoggers = new List<ILogger>();

        protected object mLoggerLock = new object();

        #endregion

        #region Public Properties

        public LogOutputLevel LogOutputLevel { get; set; }

        public bool IncludeLogOriginDetails { get; set; }

        #endregion

        #region Public Events

        public event Action<(string Message, LogLevel Level)> NewLog = (details) => { };

        #endregion

        #region Constructor

        public BaseLogFactory()
        {
            // Add console logger
            AddLogger(new ConsoleLogger());
        }
         
        #endregion

        #region Public Methods

        public void AddLogger(ILogger logger)
        {
            lock(mLoggerLock)
            {
                if(!mLoggers.Contains(logger))
                    mLoggers.Add(logger);
            }
        }

        public void RemoveLogger(ILogger logger)
        {
            lock (mLoggerLock)
            {
                if (mLoggers.Contains(logger))
                    mLoggers.Remove(logger);
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Informative, [CallerMemberName] string origin = "", [CallerFilePath] string filepath = "", [CallerLineNumber] int lineNumber = 0)
        {
            // If we should not log the message as the level is too low...
            if ((int)level < (int)LogOutputLevel)
                return;

            // If the user wants to know where the log originated from ...
            if(IncludeLogOriginDetails)
            {
                message = $"[{Path.GetFileName(filepath)} > {origin}() > Line {lineNumber}]{System.Environment.NewLine}{message}";
            }

            // Log to all loggers
            mLoggers.ForEach(logger => logger.Log(message, level));

            // Inform listeners
            NewLog.Invoke((message, level));
        }

        #endregion

    }
}
