﻿using System;
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

            //Load notifications (chats with unread messages)
            NotificationList.ItemsSource =  MatchmakerAPI_Client.LoadNotifications(currentUser).Result;
        }


        private void goBack_MouseDown(object sender, MouseButtonEventArgs e) {
            NavigationService.Navigate(new HomePage());
        }

        //Refresh the notifications and animate the refresh button
        private void RefreshNotificationsButton_MouseDown(object sender, MouseButtonEventArgs e) {
            NotificationList.ItemsSource = MatchmakerAPI_Client.LoadNotifications(currentUser).Result;

            for (int i = 0; i < 360; i += 10) {
                RotateTransform rotateTransform = new RotateTransform(i);
                RefreshNotificationsButton.RenderTransform = rotateTransform;
                Thread.Sleep(25);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        //When a chat is selected and the go-to-chat-button is pressed, go to the chat
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
    }
}
