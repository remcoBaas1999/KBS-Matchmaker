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

            LoadNotifications();
        }

        private async void LoadNotifications() {
            List<Notification> notifications = new List<Notification>();

            //All users
            var users = MatchmakerAPI_Client.GetUsers();

            foreach (KeyValuePair<string, int> user in users) {
                //Convert to UserData
                UserData userData = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(user.Key));
                
                //Run through each user and see if the current user has unread messages from them
                //If so, add a link to that chat to the notification page
                if (await UserHasNewMessages(currentUser, userData)) {
                    notifications.Add(new Notification(userData));
                }
            }
            NotificationList.ItemsSource = notifications;
        }

        private async Task<bool> UserHasNewMessages(UserData currentUser, UserData otherUser) {
            string chatID;
            if (currentUser.id < otherUser.id) {
                chatID = $"{currentUser.id}_{otherUser.id}";
            } else {
                chatID = $"{otherUser.id}_{currentUser.id}";
            }

            var messageList = MatchmakerAPI_Client.DeserializeMessageData(await MatchmakerAPI_Client.GetMessageData(chatID));

            foreach (MessageData message in messageList) {
                if (!message.seen) {
                    return true;
                }
            }
            return false;
        }

        private void goBack_MouseDown(object sender, MouseButtonEventArgs e) {
            //Go back to homepage
            NavigationService.GoBack();
        }

        private void RefreshNotificationsButton_MouseDown(object sender, MouseButtonEventArgs e) {
            LoadNotifications();

            for (int i = 0; i < 360; i += 10) {
                RotateTransform rotateTransform = new RotateTransform(i);
                RefreshNotificationsButton.RenderTransform = rotateTransform;
                Thread.Sleep(25);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void btnGoToChat_Click(object sender, RoutedEventArgs e) {
            object user = NotificationList.SelectedItem;
            if (user == null) {
                MessageBox.Show("Please select a notification!", "");
            } else {
                ChatPage chatPage = new ChatPage((user as Notification).user);
                chatPage.InitializeComponent();
                NavigationService.Navigate(chatPage);
            }
        }

        public class Notification {
            public UserData user { get; set; }
            public Notification(UserData user) {
                this.user = user;
            }
        }
    }
}
