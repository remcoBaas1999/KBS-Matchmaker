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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MatchMakerClassLibrary;

namespace Matchmaker
{
    public partial class MainWindow : Window {
        public MainWindow() {
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
                    ChangeScreen.Data = Geometry.Parse("M58.152,58.152H0V0h58.152V58.152z M3,55.152h52.152V3H3V55.152z");
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
                ChangeScreen.Data = Geometry.Parse("M58.152,58.152H0V0h58.152V58.152z M3,55.152h52.152V3H3V55.152z");
            }
        }
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

    }
    public  class activeUser : UserData
    {

        public  string realName { get; set; }
        public  string city { get; set; }
        public  DateTime Age { get; set; }
        public  string Bio { get; set; }
        public List<string> hobbies { get; set; }
        public  string email { get; set; }
        public  string password { get; set; }
        public  string salt { get; set; }
        public  int id { get; set; }
        public List<HobbyData> intresses { get; set; }

    }
}

