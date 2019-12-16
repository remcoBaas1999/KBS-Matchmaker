using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        private UserData chatPartner;
        private UserData userInChat;
        int userSender;
        private string chatID;
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
            if (userInChat.id<chatPartner.id)
            {
                chatID = $"{userInChat.id} {chatPartner.id}";
                userSender = 0;
            }
            else
            {
                chatID = $"{chatPartner.id} {userInChat.id}";
                userSender = 1;
            }
            UpdateScrollBox(); 
        }
        //with sample data for testing purposes
        public ChatPage()
        {
            //fix selfcert error
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            InitializeComponent();
            //Claudia Alvarez
            int u1= 744779591;
            //Folkert
            int u2 = 1838179466;
            chatPartner = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(u1));
            chatID = $"{u1}  {u2}";
            userInChat = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(u2));

            ChatPartnerName.Text = chatPartner.realName;
            string chatPartnerPictureString = $"https://145.44.233.207/images/users/{chatPartner.profilePicture}";          
            ChatPartnerPicture.Fill = new ImageBrush(new BitmapImage(new Uri(chatPartnerPictureString, UriKind.Absolute)));
            if (userInChat.id < chatPartner.id)
            {
                chatID = $"{userInChat.id} {chatPartner.id}";
                userSender = 0;
            }
            else
            {
                chatID = $"{chatPartner.id} {userInChat.id}";
                userSender = 1;
            }
            UpdateScrollBox();
        }
        //This will return you to the last page. Here it is the ChatListPage.
        private void Return(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
        //This method will send a message. This method will be used inside other methods.
        //Still needs connection to server.
        private async void SendMessageAsync()
        {
            //creates unixtimestamp to the current time.
            long now = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //creates message and sends it to the server. Clears inputfield afterwards
            MessageData msgD = new MessageData() { ID = chatID, Sender=userSender, TimeStamp=now, Text=ChatInput.Text };
            await MatchmakerAPI_Client.PostNewMessage(msgD);
            ChatInput.Clear();         
        }
        //This method Updates the scrollable chatbox and scrolls to the bottom.
        private void UpdateScrollBox()
        {
            MessageList.Children.Clear();
            FillScrollBox();
            ScrollViewer.ScrollToEnd();
        }
        //Fills contents of scrollable chatbox. Used inside UpdateScrollBox and initialisation.
        private void FillScrollBox()
        {
            var messageList = new List<MessageData>();
            //get message list from server
            messageList= MatchmakerAPI_Client.DeserializeMessageData(MatchmakerAPI_Client.GetMessageData(chatID));
            messageList = messageList.OrderBy(msg => msg.TimeStamp).ToList();
            string[] iDS = chatID.Split(' ');
            foreach (MessageData m in messageList)
            {
                //Changes alignment and colour to represent if it was sent or received
                var tB = new TextBlock() { Text = m.Text, FontFamily= new FontFamily("Roboto"), Foreground=Brushes.White};                
                var bTB = new Border() { Child=tB, Padding = new Thickness(10), CornerRadius = new CornerRadius(10), Margin=new Thickness(5)};                
                
                //Step by step breakdown of if-statement:
                //The MatchmakerAPI_Client will get the user data of the current user to get its ID.
                //The MatchmakerAPI_Client will deserialize this data
                //The ID will be compared to the sender (0/1) and then the position in the messageID.
                //If they match the user is the sender. Send messages align right and are purple. Received messages are gray and aligned to the left.                
                if(userInChat.id==Int32.Parse(iDS[m.Sender]))
                {
                    bTB.HorizontalAlignment = HorizontalAlignment.Right;
                    bTB.Background = Brushes.BlueViolet;
                }
                else
                {
                    bTB.HorizontalAlignment = HorizontalAlignment.Left;
                    bTB.Background = Brushes.Gray;
                }
                MessageList.Children.Add(bTB);
            }            
        }
        private void ChatInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if keypress is enter it sends the message
            if ((int) e.Key == 6 && e.KeyboardDevice.Modifiers!=ModifierKeys.Shift)
            {
                e.Handled = true;
                SendMessageAsync();
                UpdateScrollBox();
                Console.WriteLine($"{e.Key}: {(int)e.Key}");
            }
        }
    }
}
    