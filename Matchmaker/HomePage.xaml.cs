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
        private int LoggedInUserID;
        private int FirstProfileID;
        private int SecondProfileID;
        private int ThirdProfileID;
        private int FourthProfileID;

        public HomePage() {
            //Start application
            InitializeComponent();

            //Gather info about logged-in user
            String email = User.email;
            UserData activeUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(email));
            LoggedInUserID = activeUser.id;

            //Get 4 profiles on homepage
            Dictionary<String, int> Profiles = new Dictionary<string, int>();
            Profiles = MatchmakerAPI_Client.GetUsers();
            var profiles = Profiles.Values.ToList();

            //Create first profile

            Random random = new Random();
            int rnd = random.Next(0, Profiles.Count);
            FirstProfileID = profiles.ElementAt(rnd);
            while (FirstProfileID.Equals(LoggedInUserID)) {
                rnd = random.Next(0, Profiles.Count);
                FirstProfileID = profiles.ElementAt(rnd);
            }
            UserData user1 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FirstProfileID));
            //Set name
            Profile1Tag.Content = user1.realName;

            //Set profile picture           
            string pfPic1 = $"https://145.44.233.207/images/users/{user1.profilePicture}";
            ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
            //Set Cover Image
            string coverImage = $"https://145.44.233.207/images/covers/{user1.coverImage}";
            Profile1BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            //Create second profile
            rnd = random.Next(0, Profiles.Count);
            SecondProfileID = profiles.ElementAt(rnd);
            while(FirstProfileID == SecondProfileID || SecondProfileID.Equals(LoggedInUserID)) {
                rnd = random.Next(0, Profiles.Count);
                SecondProfileID = profiles.ElementAt(rnd);
            }
            UserData user2 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(SecondProfileID));
            //Set name
            Profile2Tag.Content = user2.realName;
            //Set profile picture
            string pfPic2 = $"https://145.44.233.207/images/users/{user2.profilePicture}";
            ProfilePicture2.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic2, UriKind.Absolute)));
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{user2.coverImage}";
            Profile2BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            //Create third profile
            rnd = random.Next(0, Profiles.Count);
            ThirdProfileID = profiles.ElementAt(rnd);
            while (ThirdProfileID == FirstProfileID || ThirdProfileID == SecondProfileID || ThirdProfileID.Equals(LoggedInUserID)) {
                rnd = random.Next(0, Profiles.Count);
                ThirdProfileID = profiles.ElementAt(rnd);
            }
            UserData user3 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(ThirdProfileID));
            //Set name
            Profile3Tag.Content = user3.realName;
            //Set profile picture
            string pfPic3 = $"https://145.44.233.207/images/users/{user3.profilePicture}";
            ProfilePicture3.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic3, UriKind.Absolute)));
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{user3.coverImage}";
            Profile3BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));


            //Create fourth profile
            rnd = random.Next(0, Profiles.Count);
            FourthProfileID = profiles.ElementAt(rnd);
            while (FourthProfileID == FirstProfileID || FourthProfileID == SecondProfileID || FourthProfileID == ThirdProfileID || FourthProfileID.Equals(LoggedInUserID)) {
                rnd = random.Next(0, Profiles.Count);
                FourthProfileID = profiles.ElementAt(rnd);
            }
            UserData user4 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FourthProfileID));
            //Set name
            Profile4Tag.Content = user4.realName;
            //Set profile picture
            string pfPic4 = $"https://145.44.233.207/images/users/{user4.profilePicture}";
            ProfilePicture4.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic4, UriKind.Absolute)));
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{user4.coverImage}";
            Profile4BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        /*private void RefreshButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Refresh recommended profiles
            HomePage p = new HomePage();
            p.InitializeComponent();
            NavigationService.Navigate(p);
        }*/

        //When clicked on a profile
        private void Profile1BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Page page = new UserProfile(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FirstProfileID)));
            NavigationService.Navigate(page);
        }
        private void Profile2BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            Page page = new UserProfile(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(SecondProfileID)));
            NavigationService.Navigate(page);
        }

        private void Profile3BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            Page page = new UserProfile(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(ThirdProfileID)));
            NavigationService.Navigate(page);
        }
        private void Profile4BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            Page page = new UserProfile(MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FourthProfileID)));
            NavigationService.Navigate(page);
        }


        //Menu buttons
        private void Notification_MouseDown(object sender, MouseButtonEventArgs e)
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
            Settings settings = new Settings(LoggedInUserID);
            NavigationService.Navigate(settings);
        }

        private void MyProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, User.loggedIn);
            NavigationService.Navigate(userProfile);
        }

        //Redirect links for activites
        private void Activity1Border_MouseDown(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("https://www.workshoppen.nl/workshops/action-painting-abstract-schilderen/");
        }

        private void Activity2Border_MouseDown(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("https://gamingweek.info/");
        }

        private void Activity3Border_MouseDown(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("http://www.ijsclubvzodkampen.nl/");
        }

        private void Activity4Border_MouseDown(object sender, MouseButtonEventArgs e) {
            System.Diagnostics.Process.Start("https://www.xycletracx.nl/");
        }
    }
}
