﻿using Dna;
using Fasetto.Word.Core;
using System.Threading.Tasks;
using System.Windows;

namespace Fasetto.Word
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            ApplicationSetup();

            // Log it
            IoC.Logger.Log("Application starting...", LogLevel.Debug);

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private void ApplicationSetup()
        {
            // Setup the Dna Framework
            new DefaultFrameworkConstruction()
                .Configure()
                .UseFileLogger("anewlog.txt")
                .Build();

            Task.Run((System.Func<Task>)(async () =>
            {
                var result = await WebRequests.PostAsync("http://localhost:5000/test", new SettingsDataModel { Id = "from client", Name = "Fasetto", Value = "ha" });
                var a = result;
            }));
            

            // Setup IoC
            IoC.Setup();

            // Bind a logger
            IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLogFactory(new[]
            {
                // TODO: Add ApplicationSettings so we can set/edit a log location
                //       For now just log to the path where this application is running
                new Core.FileLogger("Oldlog.txt"),
            }));

            // Add our task manager
            IoC.Kernel.Bind<ITaskManager>().ToConstant(new TaskManager());

            // Bind a file manager
            IoC.Kernel.Bind<IFileManager>().ToConstant(new FileManager());

            // Bind a UI Manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }

        /// <summary>
        /// Our Settings database table representational model
        /// </summary>
        public class SettingsDataModel
        {
            /// <summary>
            /// The unique Id for this entry
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// The settings name
            /// </summary>
            /// <remarks>This column is indexed</remarks>
            public string Name { get; set; }

            /// <summary>
            /// The settings value
            /// </summary>
            public string Value { get; set; }
        }
    }
}
