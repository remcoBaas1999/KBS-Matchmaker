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
        private UserData userPartner;
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
            FillScrollBox();

        }
        //with sample data for testing purposes
        public ChatPage()
        {
            //fix selfcert error
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            InitializeComponent();
            
            Dictionary<String, int> Profiles = new Dictionary<string, int>();
            Profiles = MatchmakerAPI_Client.GetUsers();
            var profiles = Profiles.Values.ToList();
            //pick random profile
            Random random = new Random();
            int i = random.Next(0, profiles.Count);
            int a = profiles[i];
            chatPartner = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(a));
            int b;
            if (a + 1 == profiles.Count)
            {
                b = a - 1;
            }
            else
            {
                b = a + 1;
            }
            userPartner = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(b));

            ChatPartnerName.Text = chatPartner.realName;
            string chatPartnerPictureString = $"https://145.44.233.207/images/users/{chatPartner.profilePicture}";          
            ChatPartnerPicture.Fill = new ImageBrush(new BitmapImage(new Uri(chatPartnerPictureString, UriKind.Absolute)));
            UpdateScrollBox();
        }
        //This will return you to the last page. Here it is the ChatListPage.
        private void Return(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
        //This method will send a message. This method will be used inside other methods.
        //Still needs connection to server.
        private void SendMessage()
        {
            //ServersideChatInput.Text;
            ChatInput.Clear();
        }
        //This method Updates the scrollable chatbox.
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

            //Sampledata
            string sID = $"{userPartner.id} 35";
            for(int i = 0; i < 100; i++)
            {
                messageList.Add(new MessageData { ID = sID, Text = $"Yeet #{i}", Sender = i%2, TimeStamp = i });
            }
            /*messageList.Add(new MessageData { ID = sID, Text = "Yeeteth yoinketh", Sender = 1, TimeStamp = 3 });
            messageList.Add(new MessageData { ID = sID, Text = "Yeeteth yoinketh back to you", Sender = 0, TimeStamp = 4 });
            messageList.Add(new MessageData { ID = sID, Text = "Yeeteth yoinketh back to youno2", Sender = 0, TimeStamp = 2 });
            messageList.Add(new MessageData { Text = "Alpha yeet", Sender = 0, ID = sID }) ;
            */  //Serverside msgdata
            messageList = messageList.OrderBy(msg => msg.TimeStamp).ToList();
            foreach (MessageData m in messageList)
            {
                var tB = new TextBlock() { Text = m.Text, FontFamily= new FontFamily("Roboto"), Foreground=Brushes.White};                
                var bTB = new Border() { Child=tB, Padding = new Thickness(10), CornerRadius = new CornerRadius(10), Margin=new Thickness(5)};                
                //Changes alignment and colour to represent if it was sent or received
                string[] iDS = m.ID.Split(' ');

                //Step by step breakdown of if-statement:
                //The MatchmakerAPI_Client will get the user data of the current user to get its ID.
                //The MatchmakerAPI_Client will deserialize this data
                //The ID will be compared to the sender (0/1) and then the position in the messageID.
                //If they match the user is the sender. Send messages align right and are purple. Received messages are gray and aligned to the left.
                
                if(userPartner.id==Int32.Parse(iDS[m.Sender]))//if(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email)).id.Equals(Int32.Parse(iDS[m.Sender])))
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
            if ((int) e.Key == 6 && e.KeyboardDevice.Modifiers!=ModifierKeys.Shift)
            {
                e.Handled = true;
                SendMessage();
                UpdateScrollBox();
                Console.WriteLine($"{e.Key}: {(int)e.Key}");
            }
        }
    }
}
    