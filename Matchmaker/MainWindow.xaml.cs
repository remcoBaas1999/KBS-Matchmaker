using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MatchMakerClassLibrary;

// Todo
// - add image through code
// - reverse error colors back to original

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomePage : Page {
        public HomePage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
        }
        private void Profile1Picture1_MouseDown(object sender, MouseButtonEventArgs e) { //When clicked on profilepicture, open profile
            Window Profile = new Window();
            Profile.Title = "Profile: Jeff Anderson";
            Profile.Show();

        }

    }
}
