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

namespace Matchmaker
{
    public partial class HomePage : Page {
        public HomePage() {
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e) {
        }
        private void RefreshButton_MouseDown(object sender, MouseButtonEventArgs e) {
            //Refresh recommended profiles

            //Refresh Profile1
            Profile1Tag.Content = "";
        }
        private void Profile1Picture1_MouseDown(object sender, MouseButtonEventArgs e) { 
            
            //When MouseDown on a users profilepicture
            
            //1. Open new page
            Page page = new Page();
            NavigationService.Navigate(page);

            //2. Open profile and display
            

        }

        private void Profile1BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            Profile1Picture1_MouseDown(sender, e);
        }
        public static class activeUser
        {
            public static string firstName { get; set; } = "Hans";
            public static string lastName { get; set; } = "Zimmer";
            public static string location { get; set; } = "Zwolle";
            public static DateTime birthdate { get; set; } = new DateTime(1996, 6, 3);
            public static string bio { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas bibendum dolor semper nibh ullamcorper, eu bibendum diam venenatis. Sed ut mi dui. Praesent nec efficitur neque, sed interdum mauris. Etiam suscipit dui at sem cursus, vel blandit metus tempor. Cras id turpis massa. Praesent pulvinar, velit et vestibulum consequat, sem diam convallis magna, porta ultrices sapien enim non tellus. In egestas, dolor at dictum semper, est nisi porttitor metus, sit amet posuere enim ex non magna. Suspendisse maximus libero ut lectus scelerisque viverra. Nam et tempus tellus. Mauris vel consectetur erat, a dictum eros. ";
        }

        private void Tempt_Click(object sender, RoutedEventArgs e)
        {
            Page userProfile = new UserProfile();
            NavigationService.Navigate(userProfile);
        }
    }
}
