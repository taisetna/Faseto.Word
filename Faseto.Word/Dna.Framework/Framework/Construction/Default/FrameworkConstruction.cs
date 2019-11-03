using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dna
{
    /// <summary>
    /// The construction information when starting up and configuring Dna.Framwork
    /// </summary>
    public class FrameworkConstruction
    {
        #region Public Properties

        /// <summary>
        /// the services that will get used and compiled once the framework is built
        /// </summary>
        public IServiceCollection Services { get; set; }

        public FrameworkEnvironment Enviroment { get; set; }

        public IConfiguration Configuration { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FrameworkConstruction()
        {
            // Create a new list of dependencies
            var services = new ServiceCollection();

            // Create environment details
             Enviroment = new FrameworkEnvironment();

            // Inject environment into services
            services.AddSingleton(Enviroment);
        }
        #endregion
    }
}
