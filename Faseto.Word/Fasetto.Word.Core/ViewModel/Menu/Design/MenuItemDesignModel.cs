namespace Fasetto.Word.Core
{
    public class MenuItemDesignModel : MenuItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static MenuItemDesignModel Instance => new MenuItemDesignModel();

        #endregion

        #region Constructor

        public MenuItemDesignModel()
        {
            Text = "Hello World";
            Icon = IconType.File;
        } 

        #endregion
    }
}
