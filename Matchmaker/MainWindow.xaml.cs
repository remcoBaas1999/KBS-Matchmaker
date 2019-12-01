using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoginPage login = new LoginPage();
            MainFrame.Navigate(login);
        }

        private void CloseAppClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    ChangeScreen.Data = Geometry.Parse("M 512 0 L 98.226562 0 L 98.226562 284.359375 L 0 284.359375 L 0 512 L 226.453125 512 L 226.453125 414.117188 L 512 414.117188 Z M 196.453125 481.976562 L 30 481.976562 L 30 314.386719 L 98.226562 314.386719 L 98.226562 414.117188 L 196.453125 414.117188 Z M 196.453125 384.089844 L 128.226562 384.089844 L 128.226562 314.386719 L 196.453125 314.386719 Z M 482 384.089844 L 226.453125 384.089844 L 226.453125 305.632812 L 403.328125 130.214844 L 403.328125 184.28125 L 433.328125 184.28125 L 433.328125 79.191406 L 328.320312 79.191406 L 328.320312 109.214844 L 381.878906 109.214844 L 205.277344 284.359375 L 128.226562 284.359375 L 128.226562 30.023438 L 482 30.023438 Z M 482 384.089844");
                    break;
                default:
                    WindowState = WindowState.Maximized;
                    ChangeScreen.Data = Geometry.Parse("M114.279,0v114.274H0v378.034h378.039V378.029h114.269V0H114.279z M358.346,472.615H19.692V133.966h338.654V472.615z    M472.615,358.337h-94.577V114.274H133.971V19.692h338.644V358.337z");
                    break;
            }
        }

        // Check if the size of the screen is changed. If so switch between icons if the size meets the condition.
        private void FullscreenCheck(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                ChangeScreen.Data = Geometry.Parse("M114.279,0v114.274H0v378.034h378.039V378.029h114.269V0H114.279z M358.346,472.615H19.692V133.966h338.654V472.615z    M472.615,358.337h-94.577V114.274H133.971V19.692h338.644V358.337z");
            }
            else
            {
                ChangeScreen.Data = Geometry.Parse("M 512 0 L 98.226562 0 L 98.226562 284.359375 L 0 284.359375 L 0 512 L 226.453125 512 L 226.453125 414.117188 L 512 414.117188 Z M 196.453125 481.976562 L 30 481.976562 L 30 314.386719 L 98.226562 314.386719 L 98.226562 414.117188 L 196.453125 414.117188 Z M 196.453125 384.089844 L 128.226562 384.089844 L 128.226562 314.386719 L 196.453125 314.386719 Z M 482 384.089844 L 226.453125 384.089844 L 226.453125 305.632812 L 403.328125 130.214844 L 403.328125 184.28125 L 433.328125 184.28125 L 433.328125 79.191406 L 328.320312 79.191406 L 328.320312 109.214844 L 381.878906 109.214844 L 205.277344 284.359375 L 128.226562 284.359375 L 128.226562 30.023438 L 482 30.023438 Z M 482 384.089844");
            }
            
        }
    }
}
