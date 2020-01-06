using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        private UserData chatPartner;
        private UserData userInChat;
        int timerTime = 1500;
        int userSender;
        private string chatID;
        List<MessageData> messageList = new List<MessageData>();
        //Creates a page with a chatpartner. Own data to be retrieved via User.
        public ChatPage(UserData chatPartner)
        {
            //fix selfcert error
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            InitializeComponent();
            this.chatPartner = chatPartner;
            ChatPartnerName.Text = chatPartner.realName;
            string chatPartnerPicture = $"https://145.44.233.207/images/users/{chatPartner.profilePicture}";
            ChatPartnerPicture.Fill = new ImageBrush(new BitmapImage(new Uri(chatPartnerPicture, UriKind.Absolute)));
            this.userInChat = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            //determines chatid -> id = lowerid + space + higherid
            if (int.Parse(userInChat.id) < int.Parse(chatPartner.id))
            {
                chatID = $"{userInChat.id}_{chatPartner.id}";
                userSender = 0;
            }
            else
            {
                chatID = $"{chatPartner.id}_{userInChat.id}";
                userSender = 1;
            }
            UpdateScrollBox();
            SetTimer();
        }
        //This will return you to the last page. Here it is the ChatListPage.
        private void Return(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
        //This method will send a message. This method will be used inside other methods.
        //Still needs connection to server.
        private async Task<bool> SendMessageAsync()
        {
            //creates unixtimestamp to the current time.
            long now = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //creates message and sends it to the server. Clears inputfield afterwards
            //MessageData msgD = new MessageData() { ID = chatID, Sender=userSender, TimeStamp=now, Text=ChatInput.Text };
            MessageData msgD = new MessageData() { ID=chatID, text = ChatInput.Text, timestamp = now, sender = userSender, seen = false };
            await MatchmakerAPI_Client.PostNewMessage(msgD);
            ChatInput.Clear();
            return true;
        }
        //This method Updates the scrollable chatbox and scrolls to the bottom.
        private async void UpdateScrollBox()
        {
            var newMessageList = new List<MessageData>();
            newMessageList = MatchmakerAPI_Client.DeserializeMessageData(await MatchmakerAPI_Client.GetMessageData(chatID));
            newMessageList = newMessageList.OrderBy(msg => msg.timestamp).ToList();
            if (messageList!=newMessageList) {
                messageList = newMessageList;
                MessageList.Children.Clear();
                FillScrollBox();
                ScrollViewer.ScrollToEnd();
            }
        }
        //Fills contents of scrollable chatbox. Used inside UpdateScrollBox and initialisation.
        private async void FillScrollBox()
        {
            string[] iDS = chatID.Split('_');
            int lastSender = -1;
            foreach (MessageData m in messageList)
            {
                Console.WriteLine(m.text);
                //Changes alignment and colour to represent if it was sent or received
                var tB = new TextBlock() { TextWrapping = TextWrapping.Wrap, Text = m.text, FontFamily = new FontFamily("Roboto"), Foreground = Brushes.White };
                var bTB = new Border() { Child = tB, Padding = new Thickness(10), CornerRadius = new CornerRadius(10), Margin = new Thickness(5) };

                //Step by step breakdown of if-statement:
                //The MatchmakerAPI_Client will get the user data of the current user to get its ID.
                //The MatchmakerAPI_Client will deserialize this data
                //The ID will be compared to the sender (0/1) and then the position in the messageID.
                //If they match the user is the sender. Send messages align right and are purple. Received messages are gray and aligned to the left.                
                Console.WriteLine($"{userInChat.id}=={Int32.Parse(iDS[m.sender])}");
                if (int.Parse(userInChat.id) == Int32.Parse(iDS[m.sender]))
                {
                    if (lastSender != 1)
                    {
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(m.timestamp).ToLocalTime();
                        MessageList.Children.Add(new TextBlock() { Text = $"{userInChat.realName} · {dtDateTime.ToString("HH:mm")}", FontFamily = new FontFamily("Roboto"), FontSize = 10, HorizontalAlignment = HorizontalAlignment.Right });
                    }
                    lastSender = 1;
                    bTB.HorizontalAlignment = HorizontalAlignment.Right;
                    bTB.Background = Brushes.BlueViolet;
                }
                else
                {
                    if (lastSender != 0)
                    {
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(m.timestamp).ToLocalTime();
                        MessageList.Children.Add(new TextBlock() { Text = $"{chatPartner.realName} · {dtDateTime.ToString("HH:mm")}", FontFamily = new FontFamily("Roboto"), FontSize = 10, HorizontalAlignment = HorizontalAlignment.Left });
                    }
                    lastSender = 0;
                    bTB.HorizontalAlignment = HorizontalAlignment.Left;
                    bTB.Background = Brushes.Gray;
                }
                MessageList.Children.Add(bTB);
            }
        }
        private async void ChatInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if keypress is enter it sends the message
            if ((int) e.Key == 6 && e.KeyboardDevice.Modifiers!=ModifierKeys.Shift)
            {
                e.Handled = true;
                if (ChatInput.Text != "")
                {
                    await SendMessageAsync();
                    UpdateScrollBox();
                }
            }
        }
        private async void Send_MouseDown(object sender,EventArgs e)
        {
            if (ChatInput.Text!="") 
            {
                await SendMessageAsync();
                UpdateScrollBox();
            }
        }
        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout? All unsaved changes will be permanently lost.", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do not logout
            }
            else
            {
                //do logout
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }
        }
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Settings settings = new Settings(int.Parse(userInChat.id));
            NavigationService.Navigate(settings);
        }

        private void MyProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));

            Page userProfile = new UserProfile(user, User.loggedIn, int.Parse(user.id));
            NavigationService.Navigate(userProfile);
        }
        private void Notification_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //show notification page
            Notifications notifications = new Notifications(userInChat);
            notifications.Title = "Notifations";
            NavigationService.Navigate(notifications);
        }
        
        private void SetTimer()
        {
            var timer = new System.Timers.Timer(timerTime);
            timer.Elapsed += Update;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void Update(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                UpdateScrollBox();
            });
        }
        
        private void RefreshNotificationCount(int count) {
            NotificationCountLabel.Content = count;
            if (count == 0) {
                NotificationCountCircle.Visibility = Visibility.Collapsed;
                NotificationCountLabel.Visibility = Visibility.Collapsed;
                NotificationWithNumber.Visibility = Visibility.Collapsed;
                NotificationWithoutNumber.Visibility = Visibility.Visible;
            } else {
                NotificationCountCircle.Visibility = Visibility.Visible;
                NotificationCountLabel.Visibility = Visibility.Visible;
                NotificationWithNumber.Visibility = Visibility.Visible;
                NotificationWithoutNumber.Visibility = Visibility.Collapsed;
            }
        }
        
    }
}
    