using System;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, LogLevel level)
        {
            // Save old color
            var consoleOldColor = Console.ForegroundColor;
            var consoleColor = ConsoleColor.White;
            
            // Color console based on level
            switch(level)
            {
                // Debug is blue
                case LogLevel.Debug:
                    consoleColor = ConsoleColor.Blue;
                    break;
                
                // Verbose is gray
                case LogLevel.Verbose:
                    consoleColor = ConsoleColor.Gray;
                    break;
                
                // Warning is yellow
                case LogLevel.Warning:
                    consoleColor = ConsoleColor.DarkYellow;
                    break;
                
                // Error is red
                case LogLevel.Error:
                    consoleColor = ConsoleColor.Red;
                    break;
    
                // Success is green
                case LogLevel.Success:
                    consoleColor = ConsoleColor.Green;
                    break;
            }

            // Set the desired console color
            Console.ForegroundColor = consoleColor;

            // Write message to console
            Console.WriteLine(message);

            // Reset color
            Console.ForegroundColor = consoleOldColor;
        }
    }
}
