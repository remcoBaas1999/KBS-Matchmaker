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
            //Start application
            InitializeComponent();

            //Log into users account
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
        }
        private void RefreshButton_MouseDown(object sender, MouseButtonEventArgs e) {
            //Refresh recommended profiles
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

        private void myProfile(object sender, MouseButtonEventArgs e) {
            
        }

        private void Ellipse_MouseDown_1(object sender, MouseButtonEventArgs e) {
            //show notification page
            Notifications notifications = new Notifications();
            notifications.Title = "Notifations";
            NavigationService.Navigate(notifications);
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e) {
            if (MessageBox.Show("Are you sure you want to logout? All unsaved changes will be permanently lost.", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                //do not logout
            }
            else {
                //do logout
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }
        }

        private void Settings_MouseDown(object sender, MouseButtonEventArgs e) {
            Settings settings = new Settings();
            NavigationService.Navigate(settings);
        }
    }
}
