using System;

namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseExceptionHandler : IExceptionHandler
    {
        public void HandlerError(Exception exception)
        {
            // Log it 
            // TODO: Localization of strings 
            Framework.Logger.LogCriticalSource("Unhandled exception occurred.", exception: exception); 
        }

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseExceptionHandler()
        {

        }

        #endregion
    }
}
