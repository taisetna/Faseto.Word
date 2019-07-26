using System.Collections.Generic;

namespace Fasetto.Word.Core
{
    public class MenuDesignModel : MenuViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static MenuDesignModel Instance => new MenuDesignModel();

        #endregion

        #region Constructor

        public MenuDesignModel()
        {
            Items = new List<MenuItemViewModel>(new []
            {
                new MenuItemViewModel { Type = MenuItemType.Header, Text = "Attach a file.." },
                new MenuItemViewModel { Text = "From computer", Icon = IconType.File },
                new MenuItemViewModel { Text = "From picture" , Icon = IconType.Picture },
            });
        } 

        #endregion
    }
}
