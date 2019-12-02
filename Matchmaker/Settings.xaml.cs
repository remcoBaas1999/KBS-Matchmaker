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

namespace Matchmaker {

    public partial class Settings : Page {

        public Settings() {
            InitializeComponent();
            StartUp();
        }

        private void StartUp() {
            //Display settings as saved
            if (User.showInterests) {
                PersonalInterests.IsChecked = true;
            }
            else {
                PersonalInterests.IsChecked = false;
            }
            if (User.showAttendingActivities) {
                AttendingActivities.IsChecked = true;
            }
            else {
                AttendingActivities.IsChecked = false;
            }

            if (User.wantsNotifications) {
                UseNotificationsYES.IsChecked = true;
            }
            else {
                UseNotificationsNO.IsChecked = true;
            }
        }

        private void Return(object sender, MouseButtonEventArgs e) {
            NavigationService.GoBack();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {

        }

        

        private void SaveButton_MouseDown(object sender, MouseButtonEventArgs e) {
            //Save settings

            //Show interests
            if (PersonalInterests.IsChecked == true) {
                User.showInterests = true;
            }
            else if (PersonalInterests.IsChecked == false) {
                User.showInterests = false;
            }

            //Show attending activities?
            if (AttendingActivities.IsChecked == true) {
                User.showAttendingActivities = true;
            }
            else if (AttendingActivities.IsChecked == false) {
                User.showAttendingActivities = false;
            }

            //Notification on Yes or No?
            if (UseNotificationsYES.IsChecked == true) {
                User.wantsNotifications = true;
            }
            else if(UseNotificationsNO.IsChecked == true) {
                User.wantsNotifications = false;
            }
            
        }

        //Style for SAVE button
        private void SaveButton_MouseEnter(object sender, MouseEventArgs e) {
            SaveButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 255));
        }

        private void SaveButton_MouseLeave(object sender, MouseEventArgs e) {
            SaveButton.Foreground = new SolidColorBrush(Color.FromRgb(128, 0, 176));
        }
    }
}
