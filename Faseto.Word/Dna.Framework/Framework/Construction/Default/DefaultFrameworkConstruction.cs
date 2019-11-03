namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DefaultFrameworkConstruction()
        {
            // Configure..
            this.Configure()
                // And add default services
                .UseDefaultServices();
        }

        #endregion
    }
}