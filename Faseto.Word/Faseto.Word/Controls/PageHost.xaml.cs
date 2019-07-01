using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fasetto.Word
{
    /// <summary>
    /// PageHost.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageHost : UserControl
    {
        #region Dependency Properties

        public BasePage CurrentPage
        {
            get => (BasePage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(BasePage), typeof(PageHost), new UIPropertyMetadata(CurrentPagePropertyChanged));

        #endregion

        #region Constructor
        public PageHost()
        {
            InitializeComponent();
        }
        #endregion

        #region Property Change Events
        private static void CurrentPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

            var oldPageContent = newPageFrame.Content;

            newPageFrame.Content = null;

            // Move the previous page into the old page frame
            oldPageFrame.Content = oldPageContent;

            // Animate out previous page when the Loaded event fires
            // right after this call due to moving frames
            if (oldPageContent is BasePage oldPage)
                oldPage.ShouldAnimateOut = true;

            // Set the new page content
            newPageFrame.Content = e.NewValue;
        }
        #endregion
    }
}
