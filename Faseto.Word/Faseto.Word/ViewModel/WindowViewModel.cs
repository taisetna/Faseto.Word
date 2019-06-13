using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Faseto.Word
{
    public class WindowViewModel : BaseViewModel
    {
        private Window m_Window;

        private double m_WindowMinimizedWidth { get; set; } = 400;
        private double m_WindowMinimizedHeight { get; set; } = 400;

        public double WindowMinimizedWidth { get { return m_WindowMinimizedWidth; } }
        public double WindowMinimizedHeight { get { return m_WindowMinimizedHeight; } }

        private int m_OuterMarginSize = 10;
        private int m_WindowRadius = 10;

        public int ResizeBorder { get; set; } = 6;
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }

        public int OuterMarginSize
        {
            get { return m_Window.WindowState == WindowState.Maximized ? 0 : m_OuterMarginSize; }
            set { m_OuterMarginSize = value; }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get { return m_Window.WindowState == WindowState.Maximized ? 0 : m_WindowRadius; }
            set { m_WindowRadius = value; }
        }

        public CornerRadius WindowCornerRadius {  get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight); } }

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }

        public WindowViewModel(Window _window)
        {
            m_Window = _window;

            m_Window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            MinimizeCommand = new RelayCommand(() => m_Window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => m_Window.WindowState = WindowState.Maximized);
            CloseCommand     = new RelayCommand(() => m_Window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(m_Window, GetMousePosition()));

            var resizer = new WindowResizer(m_Window);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
        public Int32 X;
        public Int32 Y;
        }

    private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(m_Window);

            return new Point(position.X + m_Window.Left, position.Y + m_Window.Top);
        }
    }
}

