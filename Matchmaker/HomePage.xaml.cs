using MatchMakerClassLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
    public partial class HomePage : Page
    {
        private int FirstProfileID;
        private int SecondProfileID;
        private int ThirdProfileID;
        private int FourthProfileID;
        public HomePage() {
            //Start application
            InitializeComponent();

            //Get 4 profiles on homepage
            Dictionary<String, int> Profiles = new Dictionary<string, int>();
            Profiles = MatchmakerAPI_Client.GetUsers();
            var profiles = Profiles.Values.ToList();

            //Create first profile

            Random random = new Random();
            int rnd = random.Next(0, Profiles.Count);
            int ProfileID1 = profiles.ElementAt(rnd);
            FirstProfileID = ProfileID1;
            UserData user1 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(ProfileID1));
            //Set name
            Profile1Tag.Content = user1.realName;

            //Set profile picture            
            try {
                string pfPic1 = $"https://145.44.233.207/images/users/{user1.profilePicture}";
                ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
            }
            catch {
                string pfPic1 = $"https://145.44.233.207/images/users/0.jpg";
                ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
            }


            //Set Cover Image

            //Create second profile
            rnd = random.Next(0, Profiles.Count);
            int ProfileID2 = profiles.ElementAt(rnd);
            SecondProfileID = ProfileID2;
            while(FirstProfileID == SecondProfileID) {
                rnd = random.Next(0, Profiles.Count);
                SecondProfileID = profiles.ElementAt(rnd);
            }
            UserData user2 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(SecondProfileID));
            //Set name
            Profile2Tag.Content = user2.realName;
            //Set profile picture
            string pfPic2 = $"https://145.44.233.207/images/users/{user2.profilePicture}";
            ProfilePicture2.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic2, UriKind.Absolute)));

            //Create third profile
            rnd = random.Next(0, Profiles.Count);
            int ProfileID3 = profiles.ElementAt(rnd);
            ThirdProfileID = ProfileID3;
            while (ThirdProfileID == FirstProfileID || ThirdProfileID == SecondProfileID) {
                rnd = random.Next(0, Profiles.Count);
                ThirdProfileID = profiles.ElementAt(rnd);
            }
            UserData user3 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(ThirdProfileID));
            //Set name
            Profile3Tag.Content = user3.realName;
            //Set profile picture
            string pfPic3 = $"https://145.44.233.207/images/users/{user3.profilePicture}";
            ProfilePicture3.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic3, UriKind.Absolute)));


            //Create fourth profile
            rnd = random.Next(0, Profiles.Count);
            int ProfileID4 = profiles.ElementAt(rnd);
            FourthProfileID = ProfileID4;
            while (FourthProfileID == FirstProfileID || FourthProfileID == SecondProfileID || FourthProfileID == ThirdProfileID) {
                rnd = random.Next(0, Profiles.Count);
                FourthProfileID = profiles.ElementAt(rnd);
            }
            UserData user4 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FourthProfileID));
            //Set name
            Profile4Tag.Content = user4.realName;
            //Set profile picture
            string pfPic4 = $"https://145.44.233.207/images/users/{user4.profilePicture}";

            ProfilePicture4.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic4, UriKind.Absolute)));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void RefreshButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Refresh recommended profiles
            HomePage p = new HomePage();
            p.InitializeComponent();
            NavigationService.Navigate(p);
        }
        private void Profile1Picture1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Page page = new UserProfile(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FirstProfileID)));
            NavigationService.Navigate(page);
        }

        private void Profile1BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Profile1Picture1_MouseDown(sender, e);
        }

        private void myProfile(object sender, MouseButtonEventArgs e)
        {

        }

        private void Ellipse_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //show notification page
            Notifications notifications = new Notifications();
            notifications.Title = "Notifations";
            NavigationService.Navigate(notifications);
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
            Settings settings = new Settings();
            NavigationService.Navigate(settings);
        }

        private void MyProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, User.loggedIn);
            NavigationService.Navigate(userProfile);
        }
    }
}
