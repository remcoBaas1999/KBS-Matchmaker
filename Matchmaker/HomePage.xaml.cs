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

            FillHomepageProfiles(GenerateFourUserDatas());
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            FillHomepageProfiles(GenerateFourUserDatas());
        }

        private UserData[] GenerateFourUserDatas() {
            UserData[] userDatas = MatchmakerAPI_Client.GetMatches(LoggedInUserID);

            /*
            //Get all user IDs
            Dictionary<String, int> Profiles = await MatchmakerAPI_Client.GetUsers();
            List<int> profiles = Profiles.Values.ToList();

            Dictionary<UserData, int> scoredUsers = new Dictionary<UserData, int>();

            //Score all users ...
            foreach (int userID in profiles) {
                //... apart from the current user
                if (userID != LoggedInUserID) {
                    UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(userID));
                    UserData currentUser = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(LoggedInUserID));

                    int hobbies = 0;
                    int age;
                    int city = 0;

                    //Add 2 points to the score for each common hobby
                    foreach (HobbyData hobby in user.hobbies) {
                        if (currentUser.hobbies.Contains(hobby)) {
                            hobbies += 2;
                        }
                    }

                    //Add max. 10 points to the score, from 10 points if the users are the same age, to 0 points if the users are 10 years apart 
                    int userAge = CalculateAge(UnixTimeToDate(user.birthdate));
                    int currentUserAge = CalculateAge(UnixTimeToDate(currentUser.birthdate));
                    int ageDiff = Math.Max(userAge, currentUserAge) - Math.Min(userAge, currentUserAge);
                    age = Math.Max((10 - ageDiff), 0);

                    //Add 10 points to the score if the cities match
                    if (user.city == currentUser.city) {
                        city = 10;
                    }

                    int score = hobbies + ageDiff + city;
                    scoredUsers.Add(user, score);
                }
            }

            //Sort users from highest to lowest score
            scoredUsers.OrderByDescending(key => key.Value);

            //Convert dictionary to list of IDs
            List<int> scoredUsersList = new List<int>();
            foreach (KeyValuePair<UserData, int> scoredUser in scoredUsers) {
                scoredUsersList.Add(scoredUser.Key.id);
            }

            //Pick 4 users randomly out of the 12 with the highst score (the first 12)
            int selectHighest = 4;//12;
            Random random = new Random();

            //Create first profile
            int rnd = random.Next(0, selectHighest);
            FirstProfileID = scoredUsersList.ElementAt(rnd);
            userDatas[0] = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FirstProfileID));
            
            //Create second profile
            rnd = random.Next(0, Profiles.Count);
            SecondProfileID = profiles.ElementAt(rnd);
            while (SecondProfileID == FirstProfileID) {
                rnd = random.Next(0, Profiles.Count);
                SecondProfileID = profiles.ElementAt(rnd);
            }
            userDatas[1] = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(SecondProfileID));
            
            //Create third profile
            rnd = random.Next(0, Profiles.Count);
            ThirdProfileID = profiles.ElementAt(rnd);
            while (ThirdProfileID == FirstProfileID || ThirdProfileID == SecondProfileID) {
                rnd = random.Next(0, Profiles.Count);
                ThirdProfileID = profiles.ElementAt(rnd);
            }
            userDatas[2] = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(ThirdProfileID));
            
            //Create fourth profile
            rnd = random.Next(0, Profiles.Count);
            FourthProfileID = profiles.ElementAt(rnd);
            while (FourthProfileID == FirstProfileID || FourthProfileID == SecondProfileID || FourthProfileID == ThirdProfileID) {
                rnd = random.Next(0, Profiles.Count);
                FourthProfileID = profiles.ElementAt(rnd);
            }
            userDatas[3] = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(FourthProfileID));
            */

            return userDatas;
        }

        private void FillHomepageProfiles(UserData[] userDatas) {
            //Set name
            Profile1Tag.Content = userDatas[0].realName;
            //Set profile picture           
            string pfPic1 = $"https://145.44.233.207/images/users/{userDatas[0].profilePicture}";
            ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
            //Set Cover Image
            string coverImage = $"https://145.44.233.207/images/covers/{userDatas[0].coverImage}";
            Profile1BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            //Set name
            Profile2Tag.Content = userDatas[1].realName;
            //Set profile picture
            string pfPic2 = $"https://145.44.233.207/images/users/{userDatas[1].profilePicture}";
            ProfilePicture2.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic2, UriKind.Absolute)));
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{userDatas[1].coverImage}";
            Profile2BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            //Set name
            Profile3Tag.Content = userDatas[2].realName;
            //Set profile picture
            string pfPic3 = $"https://145.44.233.207/images/users/{userDatas[2].profilePicture}";
            ProfilePicture3.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic3, UriKind.Absolute)));
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{userDatas[2].coverImage}";
            Profile3BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));

            //Set name
            Profile4Tag.Content = userDatas[3].realName;
            //Set profile picture
            string pfPic4 = $"https://145.44.233.207/images/users/{userDatas[3].profilePicture}";
            ProfilePicture4.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic4, UriKind.Absolute)));
            //Set Cover Image
            coverImage = $"https://145.44.233.207/images/covers/{userDatas[3].coverImage}";
            Profile4BackgroundPicture.Background = new ImageBrush(new BitmapImage(new Uri(coverImage, UriKind.Absolute)));
        }

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

        // go to the contacts page
        private void ContactPage_Click(object sender, RoutedEventArgs e)
        {
            ChatListPage chatList = new ChatListPage();
            NavigationService.Navigate(chatList);
        }
    }
}
