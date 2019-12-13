using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for ChatListPage.xaml
    /// </summary>
    public partial class ChatListPage : Page
    {
        public ChatListPage()
        {
            InitializeComponent();
            LoadChats();
        }

        private void LoadChats()
        {
            ObservableCollection<UserData> users = new ObservableCollection<UserData>();
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });
            users.Add(new UserData { realName = "Test test", profilePicture = "Images/ProfilePicture.jpg" });

            foreach (UserData user in users)
            {
                // The bar of a user
                StackPanel chatInfo = new StackPanel();
                chatInfo.Orientation = Orientation.Horizontal;
                chatInfo.Margin = new Thickness(0, 8 ,0 , 8);
                chatInfo.Height = 70;

                //// Image elipse
                //Ellipse profilePicture = new Ellipse();
                //ImageBrush profileImage = new ImageBrush();
                //profilePicture.Fill = new ImageBrush(new BitmapImage(new Uri("Images/ProfilePicture.jpg", UriKind.Relative)));
                //profilePicture.Height = 54;
                //profilePicture.Width = 54;
                //profilePicture.Margin = new Thickness(16, 0, 0, 0);
                //profilePicture.VerticalAlignment = VerticalAlignment.Center;
                StackPanel chatUser = new StackPanel();
                chatUser.Margin = new Thickness(16, 17, 16, 15);

                // Simple info
                TextBlock realName = new TextBlock();
                realName.Text = user.realName;
                realName.FontSize = 16;
                realName.LineHeight = 20;
                realName.VerticalAlignment = VerticalAlignment.Center;

                // Last message
                TextBlock lastMessage = new TextBlock();
                lastMessage.Text = "No messages received";
                lastMessage.FontSize = 16;
                lastMessage.LineHeight = 20;
                lastMessage.Opacity = 0.6;

                //chatInfo.Children.Add(profilePicture);
                chatUser.Children.Add(realName);
                chatUser.Children.Add(lastMessage);
                //chatList.Children.Add(chatUser);
             }
        }
    }
}
