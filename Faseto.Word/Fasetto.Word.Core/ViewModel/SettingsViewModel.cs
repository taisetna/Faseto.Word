﻿using System;
using System.Windows.Input;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Command

        /// <summary>
        /// The command to open the settings menu
        /// </summary>
        public ICommand OpenCommand { get; set; }

        /// <summary>
        /// The command to close the settings menu
        /// </summary>
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {
            // Create commands
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
        }

        #endregion
                
        /// <summary>
        /// Open the settings menu
        /// </summary>
        private void Open()
        {
            IoC.Application.SideMenuVisible = true;
        }

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        private void Close()
        {
            IoC.Application.SideMenuVisible = false;
        }


    }
}
