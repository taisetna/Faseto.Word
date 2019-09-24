using System;
using System.Windows.Input;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// The view model for a text entry to edit a string value
    /// </summary>
    public class TextEntryViewModel :BaseViewModel
    {
        #region Public Properties 
        /// <summary>
        /// The label to identify what this value is for
        /// </summary>
        public string Label { get; set; }

        public string OriginalText { get; set; }

        public string EditedText { get; set; }

        public bool Editing { get; set; }
        #endregion

        #region Public Commands
        
        public ICommand EditCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TextEntryViewModel()
        {
            // Create Commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

        }

        #endregion

        #region Command Methods
        public void Edit()
        {
            EditedText = OriginalText;

            Editing ^= true;
        }
        public void Save()
        {
            OriginalText = EditedText;

            Editing = false;
        }

        public void Cancel()
        { 
            Editing = false;
        }

        
        #endregion
    }
}
