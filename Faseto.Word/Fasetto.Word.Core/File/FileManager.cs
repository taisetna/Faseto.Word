using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class FileManager : IFileManager
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileManager()
        {

        }        

        public async Task WriteAllTextToFileAsync(string text, string path, bool append = false)
        {
            // TODO: Add exception catching

            // Normalize path
            path = NormalizePath(path);

            // Resolve to absolute path
            path = ResolvePath(path);

            // Lock the task
            await AsyncAwaiter.AwaitAsync(nameof(FileManager) + path, async() =>
            {
                // Run the synchronous file access as a new task
                await IoC.Task.Run(() =>
                {
                    // Write the log message to file
                    using (var fileStream = (TextWriter)new StreamWriter(File.Open(path, append ? FileMode.Append : FileMode.Create)))
                        fileStream.Write(text);
                });
            });
        }

        public string NormalizePath(string path)
        {
            // If on Windows...
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                // Replace any forward slashes with back
                return path?.Replace('/', '\\').Trim();
            // If on Linux/Mac
            else
                // Replay any \ with /
                return path?.Replace('\\', '/').Trim();
        }

        public string ResolvePath(string path)
        {
            // Resolve the path tp absolute path
            return Path.GetFullPath(path);
        }

        #endregion
    }
}
