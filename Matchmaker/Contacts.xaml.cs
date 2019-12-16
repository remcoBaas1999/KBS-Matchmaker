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
using MatchMakerClassLibrary;

namespace Matchmaker {
    
    public partial class Contacts : Page {
        private int LoggedInUserID;
        private List<string> BlockedUsers;
        public Contacts(int userid) {          
            InitializeComponent();
            LoggedInUserID = userid;

            BlockedUsers = new List<string>();
            UserData getLoggedInUserData = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(LoggedInUserID));
            foreach (var item in getLoggedInUserData.blockedUsers) {
                UserData aBlockedUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(item));
                BlockedUsers.Add(aBlockedUser.realName);
            }
            lvDataBinding.ItemsSource = BlockedUsers;
        }

        private void lvDataBinding_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            UserProfile profile = new UserProfile(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(LoggedInUserID)),false, LoggedInUserID);
            NavigationService.Navigate(profile);
        }


        //Buttons
        private void Home_Button(object sender, RoutedEventArgs e) {
            //Go to homepage
            HomePage p = new HomePage();
            p.InitializeComponent();
            NavigationService.Navigate(p);
        }

        //Go to Notification page
        private void Notification_MouseDown(object sender, MouseButtonEventArgs e) {
            Notifications notifications = new Notifications();
            notifications.Title = "Notifations";
            NavigationService.Navigate(notifications);
        }

        //Go to Logout page
        private void Logout_MouseDown(object sender, MouseButtonEventArgs e) {
            if (MessageBox.Show("Are you sure you want to logout? All unsaved changes will be permanently lost.", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                //Logout current user
                LoginPage loginPage = new LoginPage();
                NavigationService.Navigate(loginPage);
            }
        }

        //Go to Settings page
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e) {
            Settings settings = new Settings(LoggedInUserID);
            NavigationService.Navigate(settings);
        }

        //Go to own profilepage
        private void MyProfile_MouseDown(object sender, MouseButtonEventArgs e) {
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, true, LoggedInUserID);
            NavigationService.Navigate(userProfile);
        }

        
    }
}
