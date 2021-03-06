﻿using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using MediaBrush = System.Windows.Media.Brushes;
using System.Windows.Input;
using System.Linq;

namespace Matchmaker
{
    public partial class ChatListPage : Page
    {

        private UserData ClickedUser;
        private UserData LoggedInUser;

        public ChatListPage(UserData user)
        {
            LoggedInUser = user;
            InitializeComponent();
            NewRequests();
            ChatList();
            UserContacts();
            LoadBlockedUsers();
            RefreshNotificationCount(MatchmakerAPI_Client.GetNotificationCount(LoggedInUser));
        }

        private void NavigateHome(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            NavigationService.Navigate(homePage);
        }

        // Display the incoming request
        public void NewRequests()
        {
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));

            List<int> contactRequests = user.requestFrom;

            try
            {

                if (contactRequests.Count > 0 && contactRequests != null)
                {
                    foreach (int request in contactRequests)
                    {
                        UserData requestSender = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(request));

                        Grid rowBase = new Grid() { Width = 473 };
                        WrapPanel userRow = new WrapPanel() { Height = 70 };  // Inside the panel the userprofilepicture, name and the buttons to accept or decline.
                        Grid pictureBox = new Grid() { Height = 70 };
                        Ellipse userProfilePicture = new Ellipse() { Height = 54, Width = 54, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(8, 8, 0, 0) };
                        string pfPic1 = $"https://145.44.233.207/images/users/{requestSender.profilePicture}";
                        userProfilePicture.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
                        Border newUser = new Border()
                        {
                            Height = 16,
                            Width = 28,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            BorderThickness = new Thickness(2),
                            BorderBrush = MediaBrush.White,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            CornerRadius = new CornerRadius(5),
                            Margin = new Thickness(2, 0, 0, 0)
                        };
                        newUser.Background = MediaBrush.Purple;
                        newUser.Child = (new TextBlock() { Text = "NEW", FontSize = 10, TextAlignment = TextAlignment.Center, LineHeight = 14, Foreground = MediaBrush.White });
                        TextBlock profileName = new TextBlock() { Text = requestSender.realName, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(16, 8, 0, 0), FontSize = 16, LineHeight = 20 };

                        StackPanel requestButtons = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };

                        Path acceptSvg = new Path()
                        {
                            Data = Geometry.Parse("M6 10.2L2.5 6.70001C2.11 6.31001 1.49 6.31001 1.1 6.70001C0.709995 7.09001 0.709995 7.71001 1.1 8.10001L5.29 12.29C5.68 12.68 6.31 12.68 6.7 12.29L17.3 1.70001C17.69 1.31001 17.69 0.690007 17.3 0.300007C16.91 -0.0899927 16.29 -0.0899927 15.9 0.300007L6 10.2Z")
                        ,
                            Fill = MediaBrush.Black,
                            Opacity = 0.87
                        };
                        Path declineSvg = new Path()
                        {
                            Data = Geometry.Parse("M13.3 0.710001C12.91 0.320001 12.28 0.320001 11.89 0.710001L6.99997 5.59L2.10997 0.700001C1.71997 0.310001 1.08997 0.310001 0.699971 0.700001C0.309971 1.09 0.309971 1.72 0.699971 2.11L5.58997 7L0.699971 11.89C0.309971 12.28 0.309971 12.91 0.699971 13.3C1.08997 13.69 1.71997 13.69 2.10997 13.3L6.99997 8.41L11.89 13.3C12.28 13.69 12.91 13.69 13.3 13.3C13.69 12.91 13.69 12.28 13.3 11.89L8.40997 7L13.3 2.11C13.68 1.73 13.68 1.09 13.3 0.710001V0.710001Z"),
                            Fill = MediaBrush.Black,
                            Opacity = 0.87
                        };

                        Button acceptContact = new Button() { VerticalAlignment = VerticalAlignment.Center, Content = acceptSvg, Background = MediaBrush.Transparent, BorderThickness = new Thickness(0), Margin = new Thickness(0, 0, 12, 0), Name = $"_{requestSender.id}" };
                        Button declineContact = new Button() { VerticalAlignment = VerticalAlignment.Center, Content = declineSvg, Background = MediaBrush.Transparent, BorderThickness = new Thickness(0), Name = $"_{requestSender.id}" };

                        acceptContact.Click += ContactRequestAccept;
                        declineContact.Click += ContactRequestDecline;

                        requestButtons.Children.Add(acceptContact);
                        requestButtons.Children.Add(declineContact);


                        pictureBox.Children.Add(userProfilePicture);
                        pictureBox.Children.Add(newUser);
                        userRow.Children.Add(pictureBox);
                        userRow.Children.Add(profileName);
                        rowBase.Children.Add(userRow);
                        rowBase.Children.Add(requestButtons);
                        newRequestList.Children.Add(rowBase);
                    }
                }
                else
                {
                    newRequestList.Children.Add(new TextBlock() { FontSize = 14, Text = "You have no new contact requests.", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center });
                }
            }
            catch (NullReferenceException nre)
            {
                newRequestList.Children.Add(new TextBlock() { FontSize = 14, Text = "You have no new contact requests.", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center });
            }
        }

        // Display a list of all the chats that are started by the user.
        public async void ChatList()
        {
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));

            List<string> allUsers = new List<string>();


            string chatId;
            if (user.contacts != null)
            {
                foreach (string contactID in user.contacts.Keys)
                {
                    if (int.Parse(user.id) < int.Parse(contactID))
                    {
                        chatId = $"{user.id}_{contactID}";
                    }
                    else
                    {
                        chatId = $"{contactID}_{user.id}";
                    }

                    allUsers.Add(chatId);
                }
            }

            //Make use of data aquired from the chats that the user is in.
            if (allUsers.Count > 0)
            {
                foreach (string chat in allUsers)
                {
                    var messageList = new List<MessageData>();
                    //get message list from server
                    messageList = MatchmakerAPI_Client.DeserializeMessageData(await MatchmakerAPI_Client.GetMessageData(chat));
                    if (messageList.Count > 0)
                    {

                        string split = "";
                        split = chat.Replace(user.id, "");
                        var chatPartner = split.Replace("_", "");

                        UserData chatContact = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(int.Parse(chatPartner)));


                        Border rowBase = new Border() { Width = 473, Background = MediaBrush.Transparent, Name = $"_{chatContact.id}" }; // todo: mousedown event to the chat page.
                        WrapPanel userRow = new WrapPanel() { Height = 70, Name = $"_{chatContact.id}" };  // Inside the panel the userprofilepicture, name and the buttons to accept or decline.
                        Grid pictureBox = new Grid() { Height = 70 };
                        Ellipse userProfilePicture = new Ellipse() { Height = 54, Width = 54, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(8, 8, 0, 0), Name = $"_{chatContact.id}" };
                        string pfPic1 = $"https://145.44.233.207/images/users/{chatContact.profilePicture}";
                        userProfilePicture.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
                        TextBlock profileName = new TextBlock() { Text = chatContact.realName, TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 8, 0, 0), FontSize = 16, LineHeight = 20, HorizontalAlignment = HorizontalAlignment.Left, Name = $"_{chatPartner}" };
                        //TextBlock latestMessage = new TextBlock() { Text = "Most recent message", VerticalAlignment = VerticalAlignment.Center, FontSize = 16, LineHeight = 20, Opacity = 0.6, Margin = new Thickness(0, 0, 0, 15) };
                        StackPanel quickView = new StackPanel() { Margin = new Thickness(16, 8, 0, 0) };

                        quickView.Children.Add(profileName);

                        pictureBox.Children.Add(userProfilePicture);

                        userRow.Children.Add(pictureBox);
                        userRow.Children.Add(quickView);

                        rowBase.Child = userRow;
                        rowBase.Name = $"_{chatContact.id}";

                        rowBase.MouseDown += OpenChat_MouseDown;

                        chatList.Children.Add(rowBase);
                    }
                }

            }
            else
            {
                chatList.Children.Add(new TextBlock() { FontSize = 14, Text = "Sorry, but we could not find your chats.", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center });
            }
        }

        // Receives the id of the user and opens a new chat page
        private void OpenChat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var clickedChat = e.Source as FrameworkElement; // get the element that is clicked on and store this in the temp var
            string idOfClick = clickedChat.Name;
            idOfClick = idOfClick.Replace("_", ""); // _ is removed from the string

            UserData chatPartner = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(int.Parse(idOfClick))); // get the userdata from the database
            ChatPage chat = new ChatPage(chatPartner);
            NavigationService.Navigate(chat);
        }


        // Display your contacts
        public void UserContacts()
        {
            UserData loggedInUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Dictionary<string, bool> contacts = loggedInUser.contacts;

            try
            {
                if (contacts.Count > 0 && contacts != null) // check if there are contacts in the dictionary
                {
                    foreach (var contact in contacts)
                    {
                        UserData contactUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(int.Parse(contact.Key)));
                        if (contactUser != null && contact.Value == true)
                        {
                            string panelName = contactUser.id;
                            StackPanel userBlock = new StackPanel() { Margin = new Thickness(0, 20, 0, 0), Width = 120, Name = $"_{panelName}" };
                            Ellipse userProfilePicture = new Ellipse() { Height = 54, Width = 54, Name = $"_{panelName}" };
                            string pfPic1 = $"https://145.44.233.207/images/users/{contactUser.profilePicture}";
                            userProfilePicture.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
                            TextBlock userRealName = new TextBlock() { Text = contactUser.realName, FontSize = 16, LineHeight = 20, Opacity = 0.87, Width = 110, TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 8, 0, 0), TextWrapping = TextWrapping.Wrap };

                            userBlock.Children.Add(userProfilePicture);
                            userBlock.Children.Add(userRealName);
                            userBlock.MouseDown += NewChat_MouseDown;
                            recentlyAddedChats.Children.Add(userBlock);
                        }
                    }
                }
                else
                {
                    recentlyAddedChats.Children.Add(new TextBlock() { FontSize = 14, Text = "You have no contacts.", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, Width = 520 });
                }
            }
            catch (NullReferenceException)
            {
                recentlyAddedChats.Children.Add(new TextBlock() { FontSize = 14, Text = "You have no contacts.", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextAlignment = TextAlignment.Center, Width = 520 });
            }
        }

        private async void NewChat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Create a new chat.

            var clickedContact = e.Source as FrameworkElement;
            string idOfClick = clickedContact.Name;
            idOfClick = idOfClick.Replace("_", "");

            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));

            string chatId; // the id for every chat. this is a combination of the user id of the logged in user and the selected user to chat with.

            if (int.Parse(user.id) < int.Parse(idOfClick))
            {
                chatId = $"{user.id}_{idOfClick}";
            }
            else
            {
                chatId = $"{idOfClick}_{user.id}";
            }

            var messageList = new List<MessageData>();
            //get message list from server
            messageList = MatchmakerAPI_Client.DeserializeMessageData(await MatchmakerAPI_Client.GetMessageData(chatId));
            UserData newContactChat = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(int.Parse(idOfClick)));
            ClickedUser = newContactChat;

            if (messageList.Count <= 0) // open the overlay if there is no messagedata or go to the profile of the selected user.
            {
                overlay.Visibility = Visibility.Visible;
                decideOnProfile.Visibility = Visibility.Visible;
            }
            else
            {
                UserProfile contactProfile = new UserProfile(newContactChat, false, User.ID);
                NavigationService.Navigate(contactProfile);
            }
        }


        // Display the blocked users
        public void LoadBlockedUsers()
        {
            UserData loggedInUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));

            List<int> blockedUsers = loggedInUser.blockedUsers;

            if (blockedUsers.Count > 0 && blockedUsers != null) // check if the there are any blocked users
            {
                for (int i = 0; i < blockedUsers.Count; i++)
                {
                    UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(blockedUsers[i]));

                    Grid blockedUser = new Grid() { Height = 70 };
                    WrapPanel userWrapper = new WrapPanel();
                    Ellipse userProfilePicture = new Ellipse() { Height = 54, Width = 54, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 12, 8, 0) };
                    string pfPic1 = $"https://145.44.233.207/images/users/{user.profilePicture}";
                    userProfilePicture.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
                    TextBlock userRealName = new TextBlock() { Text = user.realName, FontSize = 16, LineHeight = 20, Opacity = 0.87, TextAlignment = TextAlignment.Center, Margin = new Thickness(0, 16, 0, 0), VerticalAlignment = VerticalAlignment.Center };
                    Button deblock = new Button() { Name = $"_{i}", Content = "Unblock", Background = MediaBrush.Transparent, BorderThickness = new Thickness(0), Foreground = MediaBrush.Purple, HorizontalAlignment = HorizontalAlignment.Right };
                    deblock.Click += Deblock_Click;

                    userWrapper.Children.Add(userProfilePicture);
                    userWrapper.Children.Add(userRealName);
                    blockedUser.Children.Add(userWrapper);
                    blockedUser.Children.Add(deblock);
                    blockedUserList.Children.Add(blockedUser);
                }
            }
            else
            {
                blockedUserList.Children.Add(new TextBlock() { FontSize = 14, Text = "You have blocked no users.", HorizontalAlignment = HorizontalAlignment.Center, TextAlignment = TextAlignment.Center, Width = 490 });
            }
        }

        // This method is the activation for deblocking the user.
        private async void Deblock_Click(object sender, RoutedEventArgs e)
        {
            UserData loggedInUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            List<int> blockedUsers = loggedInUser.blockedUsers;
            var number = (Button)sender; // get the name of the button that is clicked
            int listIndex = int.Parse(number.Name.Replace("_", string.Empty));
            blockedUsers.RemoveAt(listIndex);
            loggedInUser.blockedUsers = blockedUsers;

            await MatchmakerAPI_Client.SaveUser(loggedInUser);

            // after the await the page is updated and the user is not in the blocked list anymore.
            ChatListPage chatList = new ChatListPage(loggedInUser);
            NavigationService.Navigate(chatList);
        }

        // Decline the contact request
        private async void ContactRequestDecline(object sender, RoutedEventArgs e)
        {
            //Get sender ID
            var button = (Button)sender;
            int id = int.Parse(button.Name.Replace("_", string.Empty));

            UserData loggedInUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.ID)); // This person received a contact request.
            UserData userSender = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(id)); // This person that send a contact request.
            await MatchmakerAPI_Client.declineContactRequest(loggedInUser, userSender); // contact request is declined

            ChatListPage chatList = new ChatListPage(loggedInUser);
            NavigationService.Navigate(chatList);
        }

        // The contact request is accepted
        private async void ContactRequestAccept(object sender, RoutedEventArgs e)
        {
            //Get sender ID
            var button = (Button)sender;
            int id = int.Parse(button.Name.Replace("_", String.Empty));

            UserData loggedInUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.ID)); // This person received a contact request.
            UserData userSender = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(id)); // This person has send a contact request.
            await MatchmakerAPI_Client.ConfirmContactRequest(loggedInUser, userSender);


            NavigationService.Navigate(this);
        }

        //Menu buttons
        //Go to Notification page
        private void Notification_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Notifications notifications = new Notifications(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.ID)));
            notifications.Title = "Notifications";
            NavigationService.Navigate(notifications);
        }

        //Go to Logout page
        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout? All unsaved changes will be permanently lost.", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Logout current user
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }
        }

        //Go to Settings page
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Settings settings = new Settings(User.ID);
            NavigationService.Navigate(settings);
        }

        //Go to own profilepage
        private void MyProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, true, User.ID);
            NavigationService.Navigate(userProfile);
        }

        // Open the profile page 
        private void CheckoutProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            overlay.Visibility = Visibility.Collapsed;
            decideOnProfile.Visibility = Visibility.Collapsed;

            UserProfile userProfile = new UserProfile(ClickedUser, false, User.ID);
            NavigationService.Navigate(userProfile);
        }

        // Go to a new chatpage
        private void CreateChat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            overlay.Visibility = Visibility.Collapsed;
            decideOnProfile.Visibility = Visibility.Collapsed;

            ChatPage chat = new ChatPage(ClickedUser);
            NavigationService.Navigate(chat);
        }

        // Check if there are any new notifications and if so display the red dot with the notification count
        private void RefreshNotificationCount(int count)
        {
            NotificationCountLabel.Content = count;
            if (count == 0)
            {
                NotificationCountCircle.Visibility = Visibility.Collapsed;
                NotificationCountLabel.Visibility = Visibility.Collapsed;
                NotificationWithNumber.Visibility = Visibility.Collapsed;
                NotificationWithoutNumber.Visibility = Visibility.Visible;
            }
            else
            {
                NotificationCountCircle.Visibility = Visibility.Visible;
                NotificationCountLabel.Visibility = Visibility.Visible;
                NotificationWithNumber.Visibility = Visibility.Visible;
                NotificationWithoutNumber.Visibility = Visibility.Collapsed;
            }
        }
    }
}
