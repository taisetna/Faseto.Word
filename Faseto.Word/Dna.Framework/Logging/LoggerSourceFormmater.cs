using System;
using System.IO;

namespace Dna
{
    public static class LoggerSourceFormmater
    {
        public static string Format(object[] state, Exception exception)
        {
            // Get the values from the state
            var origin     = (string)state[1];
            var filePath   = (string)state[2];
            var lineNumber = (int)state[3];
            var message    = (string)state[3];

            // Get any exception message
            var exceptionMessage = exception?.ToString();

            // If we have an exception...
            if (exception == null)
                // New line between message and exception
                exceptionMessage += System.Environment.NewLine;

            // Format the message string
            return $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";
        }
    }
}
