using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Faseto.Word
{
    public class WindowViewModel : BaseViewModel
    {
        private Window m_Window;

        private int m_OuterMarginSize = 10;
        private int m_WindowRadius = 10;

        public int ResizeBorder { get; set; } = 6;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder); } }
        public int OuterMarginSize
        {
            get { return m_Window.WindowState == WindowState.Maximized ? 0 : m_OuterMarginSize; }
            set { m_OuterMarginSize = value; }
        }

        public Thickness OuterMarginThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get { return m_Window.WindowState == WindowState.Maximized ? 0 : m_WindowRadius; }
            set { m_WindowRadius = value; }
        }

        public CornerRadius WindowCornerRadius {  get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight); } }

        public ICommand MinimizedCommand { get; set; }
        public ICommand MaximizedCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }

        public WindowViewModel(Window _window)
        {
            m_Window = _window;

            m_Window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            MinimizedCommand = new RelayCommand(() => m_Window.WindowState = WindowState.Minimized);
            MaximizedCommand = new RelayCommand(() => m_Window.WindowState = WindowState.Maximized);
            CloseCommand     = new RelayCommand(() => m_Window.Close());
        }
    }
}

