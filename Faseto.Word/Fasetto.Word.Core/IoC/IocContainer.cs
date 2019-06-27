using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// 
    /// </summary>

    public static class IoC
    {
        #region Public Properties
        public static IKernel Kernal { get; private set; } = new StandardKernel();

        public static object Get<T>()
        {
            return Kernal.Get<T>();
        }
        #endregion

        #region Construction

        /// <summary>
        /// Sets 
        /// </summary>
        public static void Setup()
        {
            //Bind all required view Models
            BindViewModels();
        }

        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model
            Kernal.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        #endregion


    }
}
