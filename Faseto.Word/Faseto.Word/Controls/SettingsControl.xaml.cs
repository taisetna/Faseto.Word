using Fasetto.Word.Core;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// SettingsControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            // Set data context to settings vie model
            DataContext = IoC.Settings;
        }
    }
}
