using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Matchmaker
{
    public partial class UserProfile : Page
    {

        public UserData CurrentUser; // this holds the data of the currrent user profile.
        public static List<HobbyData> _HobbyData = new List<HobbyData>(); // static list of hobbydata??
        
        // Set the values for the profile page and decide if it is
        public UserProfile(UserData currentUser, bool ownUserProfile)
        {
            InitializeComponent();

            // Check if it is youre profile or someone else theirs.
            if (ownUserProfile)
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

            years.Text = (CalculateAge(UnixTimeToDate(currentUser.birthdate))).ToString();
            name.Text = currentUser.realName;
            showName.Text = currentUser.realName;
            city.Text = currentUser.city;
            bioText.Text = currentUser.about;

            if (currentUser.hobbies != null)
            {
                for (int i = 0; i < currentUser.hobbies.Count; i++)
                {
                    HobbyData item = currentUser.hobbies[i];
                    //add to list of Hobbies in the Xaml
                    LoadInterestWrapper(item.displayName, ownUserProfile);
                }
            }

            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser)); // Check if the userdata is not null
            string pfPic1 = $"https://145.44.233.207/images/users/{currentUser.profilePicture}";
            ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
        }
        
        // Calculate the current age of the user.
        public int CalculateAge(DateTime dob)
        {
            int age = DateTime.Today.Year - dob.Year;
            if (DateTime.Today < dob.AddYears(age)) age--;

            return age;
        }

        // Convert the Unixtime to an object of datetime
        private DateTime UnixTimeToDate(long _bday)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime bday = start.AddSeconds(_bday).ToLocalTime();
            return bday;
        }

        // open the edit name functionallities on screen.
        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            showName.Visibility = Visibility.Collapsed;
            editName.Visibility = Visibility.Collapsed;
            name.Visibility = Visibility.Visible;
            confirmNameChange.Visibility = Visibility.Visible;
            denymNameChange.Visibility = Visibility.Visible;
        }


        // confirm the name change and hide the buttons and the textbox on the page.
        private async void ConfirmNameChange_Click(object sender, RoutedEventArgs e)
        {

            if (name.Text.Trim() == "" || name.Text == null)
            {
                MessageBox.Show("The new name cannot be empty");
                name.Text = CurrentUser.realName;
            }
            else
            {
                CurrentUser.realName = name.Text;
                showName.Text = name.Text;
                CurrentUser.realName = name.Text;
                showName.Visibility = Visibility.Visible;
                editName.Visibility = Visibility.Visible;
                name.Visibility = Visibility.Collapsed;
                confirmNameChange.Visibility = Visibility.Collapsed;
                denymNameChange.Visibility = Visibility.Collapsed;
                await MatchmakerAPI_Client.SaveUser(CurrentUser);
            }
        }

        // Hide the edit tools and make the old name return.
        private void DenymNameChange_Click(object sender, RoutedEventArgs e)
        {
            name.Text = showName.Text;
            showName.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Collapsed;
            confirmNameChange.Visibility = Visibility.Collapsed;
            denymNameChange.Visibility = Visibility.Collapsed;
        }

        // Confirm the change to the about text, upload to the database and hide the edit tools. 
        private async void ConfirmAboutChange_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.about = accountText.Text;
            bioText.Text = accountText.Text;
            denyBioChange.Visibility = Visibility.Collapsed;
            confirmBioChange.Visibility = Visibility.Collapsed;
            accountText.Visibility = Visibility.Collapsed;
            bioText.Visibility = Visibility.Visible;
            editBio.Visibility = Visibility.Visible;
            await MatchmakerAPI_Client.SaveUser(CurrentUser);
        }

        // Keep the old text and collapse the edit tools.
        private void DenyAboutChange_Click(object sender, RoutedEventArgs e)
        {
            accountText.Text = bioText.Text;
            denyBioChange.Visibility = Visibility.Collapsed;
            confirmBioChange.Visibility = Visibility.Collapsed;
            accountText.Visibility = Visibility.Collapsed;
            bioText.Visibility = Visibility.Visible;
            editBio.Visibility = Visibility.Visible;
        }

        // Make the edit tools for the about text vissible.
        private void EditAbout_Click(object sender, RoutedEventArgs e)
        {
            denyBioChange.Visibility = Visibility.Visible;
            confirmBioChange.Visibility = Visibility.Visible;
            accountText.Visibility = Visibility.Visible;
            bioText.Visibility = Visibility.Collapsed;
            editBio.Visibility = Visibility.Collapsed;
            accountText.Text = CurrentUser.about;
        }

        // Populate the combobox citySelection.
        // This currently is a list with hard coded locations.
        private void CitySelection_Loaded(object sender, RoutedEventArgs e)
        {
            citySelection.Text = city.Text;
            foreach (string item in new List<string> { "Zwolle", "Amsterdam", "Utrecht", "Emmeloord" })
            {
                citySelection.Items.Add(item);
            }

        }

        // Makes the edit tools vissible.
        private void EditLocation_Click(object sender, RoutedEventArgs e)
        {
            citySelection.SelectedItem = city.Text;
            confirmNewLocation.Visibility = Visibility.Visible;
            denyLocationChange.Visibility = Visibility.Visible;
            editLocation.Visibility = Visibility.Collapsed;
            citySelection.Visibility = Visibility.Visible;
            city.Visibility = Visibility.Collapsed;
        }

        // Change the location of the current user.
        private async void ConfirmNewLocation_Click(object sender, RoutedEventArgs e)
        {
            city.Text = citySelection.SelectedItem.ToString();
            CurrentUser.city = citySelection.SelectedItem.ToString();
            confirmNewLocation.Visibility = Visibility.Collapsed;
            denyLocationChange.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Visible;
            citySelection.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Visible;
            await MatchmakerAPI_Client.SaveUser(CurrentUser);
        }

        // Do not make changes to the user location.
        private void DenyLocationChange_Click(object sender, RoutedEventArgs e)
        {
            citySelection.SelectedValue = city.Text;
            confirmNewLocation.Visibility = Visibility.Collapsed;
            denyLocationChange.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Visible;
            citySelection.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Visible;
        }

        // Go back to the homepage
        private void ReturnToHomepage(object sender, MouseButtonEventArgs e)
        {
            HomePage page = new HomePage();
            NavigationService.Navigate(page);
        }

        // Open the interests dialog.
        private void InterestDialog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Visible;
            entryHobbies.Visibility = Visibility.Visible;
            addInterests.Visibility = Visibility.Visible;
            listPossibleInterests.Visibility = Visibility.Visible;

            LoadInterests();
        }

        // Create a visual list of all the interests.
        private void LoadInterests()
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
                if (CurrentUser.hobbies != null)
                {
                    for (int x = 0; x < CurrentUser.hobbies.Count; x++)
                    {
                        HobbyData item = CurrentUser.hobbies[x];
                        if (listHobbies[i].displayName == item.displayName)
                        {
                            cb.IsChecked = true;
                        }
                    }
                }
                cb.Click += new RoutedEventHandler(AddInterestToList_Click);
                cb.HorizontalAlignment = HorizontalAlignment.Right;
                cb.VerticalAlignment = VerticalAlignment.Center;

                hobbyLane.Children.Add(tb);
                hobbyLane.Children.Add(cb);
                hobbyLane.Height = 25;
                hobbyLane.HorizontalAlignment = HorizontalAlignment.Stretch;

                listPossibleInterests.Children.Add(hobbyLane);
            }
        }

        // Change the coverimage of the current user.
        private void BtnEditCoverImage_Click(object sender, RoutedEventArgs e)
        {
            CoverImageSelecter coverImageSelecter = new CoverImageSelecter();
            coverImageSelecter.Show();
            coverImageSelecter.userID = CurrentUser.id;
        }

        // Add the selected interest to a list.
        public void AddInterestToList_Click(object sender, RoutedEventArgs e)
        {
            List<HobbyData> listAllHobbies = MatchmakerAPI_Client.getAllHobbies();
            string name = (e.Source as CheckBox).Name.ToString();
            string idName = name.Substring(2);
            int id = Int32.Parse(idName);

            if ((sender as CheckBox).IsChecked == true)
            {    
                _HobbyData.Add(listAllHobbies[id]);
            }
            else
            {
                _HobbyData.Remove(listAllHobbies[id]);
            }
            
        }

        // Hide the interests selection window.
        private void CancelHobbyDialog_Click(object sender, RoutedEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Collapsed;
            entryHobbies.Visibility = Visibility.Collapsed;
            addInterests.Visibility = Visibility.Collapsed;
            listPossibleInterests.Visibility = Visibility.Collapsed;
        }

        // Show all interests from the current user.
        private void LoadInterestWrapper(string hobby, bool myProfile)
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
                path.Name = $"remove{FormatName(hobby)}";
                remove.MouseDown += RemoveInterest_MouseDown;
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

        //
        private string FormatName(string hobby)
        {
            string name = hobby.Replace(" ", "_");
            name = name.Replace(":", "KOLON");
            name = name.Replace("&", "AND");
            return name;
        }

        //
        private string RestoreName(string name)
        {
            string hobby = name.Substring(6).Replace("_", " ");
            hobby = hobby.Replace("KOLON", ":");
            hobby = hobby.Replace("AND", "&");
            return hobby;
        }

        // Remove the selected interest from the user.
        private async void RemoveInterest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string name = (e.Source as Path).Name.ToString();
            string hobby = RestoreName(name);
            for (int i = 0; i < CurrentUser.hobbies.Count; i++)
            {
                if (CurrentUser.hobbies[i].displayName == hobby)
                {
                    CurrentUser.hobbies.Remove(CurrentUser.hobbies[i]);
                }
            }
            
            await MatchmakerAPI_Client.SaveUser(CurrentUser);
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            Page userProfile = new UserProfile(user, User.loggedIn);
            NavigationService.Navigate(userProfile);
        }

        // Add interest to the user.
        private async void AddInterests_Click(object sender, RoutedEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Collapsed;
            entryHobbies.Visibility = Visibility.Collapsed;
            addInterests.Visibility = Visibility.Collapsed;
            listPossibleInterests.Visibility = Visibility.Collapsed;
            bool inAccount;
            foreach (HobbyData item in _HobbyData)
            {
                inAccount = false;
                foreach (var hobby in CurrentUser.hobbies)
                {
                    if (hobby.displayName == item.displayName)
                    {
                        inAccount = true;
                    }
                }
                if (inAccount == false)
                {
                    CurrentUser.hobbies.Add(item);
                }
            }
            await MatchmakerAPI_Client.SaveUser(CurrentUser);
            UserData user = MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(User.email));
            NavigationService.Navigate(new UserProfile(user, User.loggedIn));
        }
    }
}
