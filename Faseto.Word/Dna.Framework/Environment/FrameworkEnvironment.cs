namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameworkEnvironment
    {
        #region Public Properties

        public bool IsDevelopment { get; set; } = true;

        public string Configuration => IsDevelopment ? "Development" : "Production";

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FrameworkEnvironment()
        {
#if RELEASE
            IsDevelopment = false;
#endif

        }
        #endregion
    }
}
