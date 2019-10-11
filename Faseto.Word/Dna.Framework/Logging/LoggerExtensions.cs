using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace Dna
{
    public static class LoggerExtensions
    {
         public static void LogCriticalSource(
             this ILogger logger,
             string message,
             EventId eventId = new EventId(),
             Exception exception = null,
             [CallerMemberName] string orgin = "",
             [CallerFilePath] string filePath = "",
             [CallerLineNumber] int lineNumber = 0,
             params object[] args) => logger.Log(LogLevel.Critical, message, eventId, args.Prepend(orgin, filePath, lineNumber, message), exception, LoggerSourceFormmater.Format);
    }
}
