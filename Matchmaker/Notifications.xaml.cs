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
using System.Threading;
using MatchMakerClassLibrary;

namespace Matchmaker {

    public partial class Notifications : Page {
        private UserData currentUser;
        public Notifications(UserData currentUser) {
            InitializeComponent();

            this.currentUser = currentUser;

            NotificationList.ItemsSource =  MatchmakerAPI_Client.LoadNotifications(currentUser).Result;
        }

        private void goBack_MouseDown(object sender, MouseButtonEventArgs e) {
            //Go back to homepage
            NavigationService.Navigate(new HomePage());
        }

        private void RefreshNotificationsButton_MouseDown(object sender, MouseButtonEventArgs e) {
            NotificationList.ItemsSource = MatchmakerAPI_Client.LoadNotifications(currentUser).Result;

            //Display animation for refresh
            for (int i = 0; i < 360; i += 10) {
                RotateTransform rotateTransform = new RotateTransform(i);
                RefreshNotificationsButton.RenderTransform = rotateTransform;
                Thread.Sleep(25);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void btnGoToChat_Click(object sender, RoutedEventArgs e) {
            //Go to the chat where the notification is at
            object user = NotificationList.SelectedItem;
            if (user == null) {
                MessageBox.Show("Please select a notification!", "");
            } else {
                ChatPage chatPage = new ChatPage((user as Notification).user);
                chatPage.InitializeComponent();
                NavigationService.Navigate(chatPage);
            }
        }
    }
}
