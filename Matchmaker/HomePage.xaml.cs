using MatchMakerClassLibrary;
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
    public partial class HomePage : Page {
        private UserData LoggedInUser;
        private UserData FirstProfile;
        private UserData SecondProfile;
        private UserData ThirdProfile;
        private UserData FourthProfile;


        public HomePage() {
            //Start application
            InitializeComponent();

            //Gather info about logged-in user
            String email = User.email;
            UserData getLoggedInUserData = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(email));
            LoggedInUser = getLoggedInUserData;
            User.ID = LoggedInUser.id;

            //Get userprofiles from databases
            Dictionary<String, int> Profiles = new Dictionary<string, int>();
            Profiles = MatchmakerAPI_Client.GetUsers();
            var profiles = Profiles.Values.ToList();

            //Remove blocked user(s) if any

            if (getLoggedInUserData.blockedUsers == null) {
                FillEmptyList(getLoggedInUserData);
            }

            foreach (var item in getLoggedInUserData.blockedUsers) {
                if (profiles.Contains(item)) {
                    profiles.Remove(item);
                }
            }

            ////Create first profile
            //Random random = new Random();
            //int rnd = random.Next(0, profiles.Count);
            //FirstProfileID = profiles.ElementAt(rnd);
            //while (FirstProfileID.Equals(LoggedInUserID)) {
            //    rnd = random.Next(0, profiles.Count);
            //    FirstProfileID = profiles.ElementAt(rnd);
            //}
            //UserData user1 = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FirstProfileID));
            ////Set name
            //Profile1Tag.Content = user1.realName;
            FillHomepageProfiles(GenerateUserDatas());
        }

        private void Button_FillProfiles(object sender, RoutedEventArgs e) {
            FillHomepageProfiles(GenerateUserDatas());
        }

        private UserData[] GenerateUserDatas() {
            UserData[] userDatas = MatchmakerAPI_Client.GetMatches(LoggedInUser.id);
            Console.WriteLine("\nThese are the users:");
            foreach (UserData user in userDatas) {
                Console.WriteLine($" - {user.id} ({user.realName})");
            }
            return userDatas;
        }

        private void FillHomepageProfiles(UserData[] userDatas) {
            RefreshNotificationCount(MatchmakerAPI_Client.GetNotificationCount(LoggedInUser));

            FirstProfile = userDatas[0];
            //Set name
            Profile1Tag.Content = userDatas[0].realName;
            //Set profile picture    
            ProfilePicture1.Fill = MatchmakerAPI_Client.GetProfilePicture(userDatas[0]);
            //Set Cover Image
            string coverImage = $"https://145.44.233.207/images/covers/{userDatas[0].coverImage}";
            Profile1BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            SecondProfile = userDatas[1];
            //Set name
            Profile2Tag.Content = userDatas[1].realName;
            //Set profile picture
            ProfilePicture2.Fill = MatchmakerAPI_Client.GetProfilePicture(userDatas[1]);
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{userDatas[1].coverImage}";
            Profile2BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            ThirdProfile = userDatas[2];
            //Set name
            Profile3Tag.Content = userDatas[2].realName;
            //Set profile picture
            ProfilePicture3.Fill = MatchmakerAPI_Client.GetProfilePicture(userDatas[2]);
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{userDatas[2].coverImage}";
            Profile3BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            FourthProfile = userDatas[3];
            //Set name
            Profile4Tag.Content = userDatas[3].realName;
            //Set profile picture
            ProfilePicture4.Fill = MatchmakerAPI_Client.GetProfilePicture(userDatas[3]);
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{userDatas[3].coverImage}";
            Profile4BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));
        }
        public async void FillEmptyList(UserData getLoggedInUserData) {
            List<int> temporarilyList = new List<int>();
            getLoggedInUserData.blockedUsers = temporarilyList;
            await MatchmakerAPI_Client.SaveUser(getLoggedInUserData);
        }

        //Refresh button
        private void Button_Click(object sender, RoutedEventArgs e) {
            //Refresh recommended profiles
            HomePage p = new HomePage();
            p.InitializeComponent();
            NavigationService.Navigate(p);
        }

        //When clicked on a profile
        private void Profile1BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            ButtonPressed(FirstProfile);
        }
        private void Profile2BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            ButtonPressed(SecondProfile);
        }
        private void Profile3BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            ButtonPressed(ThirdProfile);
        }
        private void Profile4BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            ButtonPressed(FourthProfile);
        }
        private void ButtonPressed(UserData userData) {
            //Console.WriteLine($"\nUser ID: {userData.id}, name: {userData.realName}, User ID is correct: {id == userData.id}");
            Page page = new UserProfile(userData, false, LoggedInUser.id);
            NavigationService.Navigate(page);
        }

        //Menu buttons
        //Go to Notification page
        private void Notification_MouseDown(object sender, MouseButtonEventArgs e) {
            Notifications notifications = new Notifications(LoggedInUser);
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
            Settings settings = new Settings(LoggedInUser.id);
            NavigationService.Navigate(settings);
        }

        //Go to own profilepage
        private void MyProfile_MouseDown(object sender, MouseButtonEventArgs e) {
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, true, LoggedInUser.id);
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

        // go to the contacts page
        private void ContactPage_Click(object sender, RoutedEventArgs e) {
            ChatListPage chatList = new ChatListPage();
            NavigationService.Navigate(chatList);
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
