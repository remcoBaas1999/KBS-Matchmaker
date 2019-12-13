using MatchMakerClassLibrary;
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
using static Matchmaker.HomePage;

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    /// 



    public partial class UserProfile : Page
    {

        UserData userInView;
        public static List<HobbyData> hobbyData = new List<HobbyData>();
        public UserProfile(UserData user, bool userAccount)
        {
            InitializeComponent();

            if(userAccount)
            {
                editBio.Visibility = Visibility.Visible;
                editLocation.Visibility = Visibility.Visible;
                editName.Visibility = Visibility.Visible;
                btnEditCoverImage.Visibility = Visibility.Visible;
                addHobby.Visibility = Visibility.Visible;
            }
            else
            {
                editBio.Visibility = Visibility.Collapsed;
                editLocation.Visibility = Visibility.Collapsed;
                editName.Visibility = Visibility.Collapsed;
                btnEditCoverImage.Visibility = Visibility.Collapsed;
                addHobby.Visibility = Visibility.Collapsed;
            }

            years.Text = (CalculateAge(UnixTimeToDate(user.birthdate))).ToString();
            name.Text = user.realName;
            showName.Text = user.realName;
            city.Text = user.city;
            bioText.Text = user.about;
            if (user.hobbies != null)
            {
                foreach (var item in user.hobbies)
                {
                    //add to list of Hobbies in the Xaml
                    LoadHobbyWrapper(item.displayName, userAccount);
                }
            }
            userInView= user;
            string pfPic1 = $"https://145.44.233.207/images/users/{user.profilePicture}";
            ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
        }
        
        // Calculate the current age of the user.
        public int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (today < dob.AddYears(age)) age--;

            return age;
        }

        // Convert the Unixtime to an object of datetime
        private DateTime UnixTimeToDate(long _bday)
        {
            
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime bday = start.AddSeconds(_bday).ToLocalTime();
            return bday;
        }

        private void editName_Click(object sender, RoutedEventArgs e)
        {
            showName.Visibility = Visibility.Collapsed;
            editName.Visibility = Visibility.Collapsed;
            name.Visibility = Visibility.Visible;
            confirmNameChange.Visibility = Visibility.Visible;
            denymNameChange.Visibility = Visibility.Visible;
        }


        private async void confirmNameChange_Click(object sender, RoutedEventArgs e)
        {

            if (name.Text.Trim() == "" || name.Text == null)
            {
                MessageBox.Show("The new name cannot be empty");
                name.Text = userInView.realName;
            }
            else if (name.Text != null && name.Text.Trim() != "")
            {
                userInView.realName = name.Text;
                showName.Text = name.Text;
                userInView.realName = name.Text;
                showName.Visibility = Visibility.Visible;
                editName.Visibility = Visibility.Visible;
                name.Visibility = Visibility.Collapsed;
                confirmNameChange.Visibility = Visibility.Collapsed;
                denymNameChange.Visibility = Visibility.Collapsed;
                await MatchmakerAPI_Client.SaveUser(userInView);
            }
            
        }

        private void denymNameChange_Click(object sender, RoutedEventArgs e)
        {
            name.Text = showName.Text;
            showName.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Collapsed;
            confirmNameChange.Visibility = Visibility.Collapsed;
            denymNameChange.Visibility = Visibility.Collapsed;
        }

        private async void confirmBioChange_Click(object sender, RoutedEventArgs e)
        {
            userInView.about = accountText.Text;
            bioText.Text = accountText.Text;
            denyBioChange.Visibility = Visibility.Collapsed;
            confirmBioChange.Visibility = Visibility.Collapsed;
            accountText.Visibility = Visibility.Collapsed;
            bioText.Visibility = Visibility.Visible;
            editBio.Visibility = Visibility.Visible;
            await MatchmakerAPI_Client.SaveUser(userInView);

        }

        private void denyBioChange_Click(object sender, RoutedEventArgs e)
        {
            accountText.Text = bioText.Text;
            denyBioChange.Visibility = Visibility.Collapsed;
            confirmBioChange.Visibility = Visibility.Collapsed;
            accountText.Visibility = Visibility.Collapsed;
            bioText.Visibility = Visibility.Visible;
            editBio.Visibility = Visibility.Visible;
        }

        private void editBio_Click(object sender, RoutedEventArgs e)
        {
            denyBioChange.Visibility = Visibility.Visible;
            confirmBioChange.Visibility = Visibility.Visible;
            accountText.Visibility = Visibility.Visible;
            bioText.Visibility = Visibility.Collapsed;
            editBio.Visibility = Visibility.Collapsed;
            accountText.Text = userInView.about;
        }

        private void citySelection_Loaded(object sender, RoutedEventArgs e)
        {
            citySelection.Text = city.Text;
            List<string> locations = new List<string> { "Zwolle", "Amsterdam", "Utrecht", "Emmeloord" };
            foreach (string item in locations)
            {
                citySelection.Items.Add(item);
            }

        }

        private void editLocation_Click(object sender, RoutedEventArgs e)
        {
            citySelection.SelectedItem = city.Text;
            confirmNewLocation.Visibility = Visibility.Visible;
            denyLocationChange.Visibility = Visibility.Visible;
            editLocation.Visibility = Visibility.Collapsed;
            citySelection.Visibility = Visibility.Visible;
            city.Visibility = Visibility.Collapsed;
        }

        private async void confirmNewLocation_Click(object sender, RoutedEventArgs e)
        {
            city.Text = citySelection.SelectedItem.ToString();
            userInView.city = citySelection.SelectedItem.ToString();
            confirmNewLocation.Visibility = Visibility.Collapsed;
            denyLocationChange.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Visible;
            citySelection.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Visible;
            await MatchmakerAPI_Client.SaveUser(userInView);
        }

        private void denyLocationChange_Click(object sender, RoutedEventArgs e)
        {
            citySelection.SelectedValue = city.Text;
            confirmNewLocation.Visibility = Visibility.Collapsed;
            denyLocationChange.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Visible;
            citySelection.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Visible;
        }
        private void Return(object sender, MouseButtonEventArgs e)
        {
            HomePage page = new HomePage();
            NavigationService.Navigate(page);
        }
        private void addHobby_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Visible;
            entryHobbies.Visibility = Visibility.Visible;
            addInterests.Visibility = Visibility.Visible;
            listPossibleInterests.Visibility = Visibility.Visible;

            LoadHobbies();
        }

        private void LoadHobbies()
        {
            List<HobbyData> listHobbies = MatchmakerAPI_Client.getAllHobbies();
            for (int i = 0; i < listHobbies.Count(); i++)
            {
                CheckBox cb = new CheckBox();
                TextBlock tb = new TextBlock();
                Grid hobbyLane = new Grid();

                tb.Text = listHobbies[i].displayName;
                tb.FontSize = 14;
                tb.Name = $"tb{i}";
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                cb.Name = $"cb{i}";
                if (userInView.hobbies != null)
                {
                    foreach (var item in userInView.hobbies)
                    {
                        if (listHobbies[i].displayName == item.displayName)
                        {
                            cb.IsChecked = true;
                        }
                    }
                }
                cb.Click += new RoutedEventHandler(addHobbyToList_Click);
                cb.HorizontalAlignment = HorizontalAlignment.Right;
                cb.VerticalAlignment = VerticalAlignment.Center;

                hobbyLane.Children.Add(tb);
                hobbyLane.Children.Add(cb);
                hobbyLane.Height = 25;
                hobbyLane.HorizontalAlignment = HorizontalAlignment.Stretch;

                listPossibleInterests.Children.Add(hobbyLane);
            }
        }
        private void btnEditCoverImage_Click(object sender, RoutedEventArgs e) {
            CoverImageSelecter coverImageSelecter = new CoverImageSelecter();
            coverImageSelecter.Show();
            coverImageSelecter.userID = userInView.id;
        }

        public void addHobbyToList_Click(object sender, RoutedEventArgs e)
        {
            List<HobbyData> listAllHobbies = MatchmakerAPI_Client.getAllHobbies();
            string name = (e.Source as CheckBox).Name.ToString();
            string idName = name.Substring(2);
            int id = Int32.Parse(idName);

            if ((sender as CheckBox).IsChecked == true)
            {    
                hobbyData.Add(listAllHobbies[id]);
            }
            else
            {
                hobbyData.Remove(listAllHobbies[id]);
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Collapsed;
            entryHobbies.Visibility = Visibility.Collapsed;
            addInterests.Visibility = Visibility.Collapsed;
            listPossibleInterests.Visibility = Visibility.Collapsed;
        }

        private void LoadHobbyWrapper(string hobby, bool myProfile)
        {
            
            Border hobbyBorder = new Border();
            StackPanel stackPanel = new StackPanel();
            TextBlock hobbyText = new TextBlock();
            Canvas remove = new Canvas();
            Path path = new Path();

            hobbyBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#673AB7"));
            hobbyBorder.Child = stackPanel;
            hobbyBorder.CornerRadius = new CornerRadius(16);
            hobbyBorder.Height = 32;
            hobbyBorder.Margin = new Thickness(6, 0, 6, 0);


            stackPanel.Orientation = Orientation.Horizontal;

            hobbyText.Text = hobby;
            hobbyText.FontSize = 14;
            hobbyText.Foreground = new SolidColorBrush(Colors.White);
            hobbyText.Margin = new Thickness(6, 0 , 6, 0);
            hobbyText.TextAlignment = TextAlignment.Center;
            hobbyText.VerticalAlignment = VerticalAlignment.Center;

            if (myProfile)
            {
                path.Data = Geometry.Parse("M13.2997 0.710001C12.9097 0.320001 12.2797 0.320001 11.8897 0.710001L6.99973 5.59L2.10973 0.700001C1.71973 0.310001 1.08973 0.310001 0.699727 0.700001C0.309727 1.09 0.309727 1.72 0.699727 2.11L5.58973 7L0.699727 11.89C0.309727 12.28 0.309727 12.91 0.699727 13.3C1.08973 13.69 1.71973 13.69 2.10973 13.3L6.99973 8.41L11.8897 13.3C12.2797 13.69 12.9097 13.69 13.2997 13.3C13.6897 12.91 13.6897 12.28 13.2997 11.89L8.40973 7L13.2997 2.11C13.6797 1.73 13.6797 1.09 13.2997 0.710001Z");
                path.Fill = Brushes.White;
                path.Name = $"remove{formatName(hobby)}";
                remove.MouseDown += Remove_MouseDown;
                remove.Children.Add(path);
                remove.Height = 15;
                remove.Width = 15;
                remove.Margin = new Thickness(6, 0, 6, 0);
                remove.HorizontalAlignment = HorizontalAlignment.Right;
                remove.VerticalAlignment = VerticalAlignment.Center;
            }

            stackPanel.Children.Add(hobbyText);
            stackPanel.Children.Add(remove);
            HobbyWrapper.Children.Add(hobbyBorder);


        }

        private string formatName(string hobby)
        {
            string name = (hobby).Replace(" ", "_");
            name = name.Replace(":", "KOLON");
            name = name.Replace("&","AND");
            return name;
        }

        private string restoreName(string name)
        {
            string hobby = name.Substring(6).Replace("_", " ");
            hobby = hobby.Replace("KOLON", ":");
            hobby = hobby.Replace("AND" ,"&");
            return hobby;
        }

        private async void Remove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string name = (e.Source as Path).Name.ToString();
            string hobby = restoreName(name);
            for (int i = 0; i < userInView.hobbies.Count; i++)
            {
                if (userInView.hobbies[i].displayName == hobby)
                {
                    userInView.hobbies.Remove(userInView.hobbies[i]);
                }
            }
            
            await MatchmakerAPI_Client.SaveUser(userInView);
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, User.loggedIn);
            NavigationService.Navigate(userProfile);
        }

        private async void AddInterests_Click(object sender, RoutedEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Collapsed;
            entryHobbies.Visibility = Visibility.Collapsed;
            addInterests.Visibility = Visibility.Collapsed;
            listPossibleInterests.Visibility = Visibility.Collapsed;
            bool inAccount;
            foreach (var item in hobbyData)
            {
                inAccount = false;
                foreach (var hobby in userInView.hobbies)
                {
                    if (hobby.displayName == item.displayName)
                    {
                        inAccount = true;
                    }
                }
                if (inAccount == false)
                {
                    userInView.hobbies.Add(item);
                }
            }
            await MatchmakerAPI_Client.SaveUser(userInView);
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, User.loggedIn);
            NavigationService.Navigate(userProfile);
        }


    }
}
