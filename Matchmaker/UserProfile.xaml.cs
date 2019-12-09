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
        public UserProfile(UserData user)
        {
            InitializeComponent();

            editBio.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Collapsed;
            editName.Visibility = Visibility.Collapsed;            
            years.Text = (CalculateAge(UnixTimeToDate(user.birthdate))).ToString();
            btnEditCoverImage.Visibility = Visibility.Collapsed;
            name.Text = user.realName;
            showName.Text = user.realName;
            city.Text = user.location;
            bioText.Text = user.about;
            if (user.hobbies != null)
            {
                foreach (var item in user.hobbies)
                {
                    //add to list of Hobbies in the Xaml
                    MessageBox.Show(item.ToString());
                    LoadHobbyWrapper(item.displayName);
                }
            }
            userInView= user;
            string pfPic1 = $"https://145.44.233.207/images/users/{user.profilePicture}";
            ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
        }
        
        public UserProfile(UserData active, bool userAccount)
        {
            InitializeComponent();

            if (userAccount)
            {
                editBio.Visibility = Visibility.Visible;
                editLocation.Visibility = Visibility.Visible;
                editName.Visibility = Visibility.Visible;
            }
            else
            {
                editBio.Visibility = Visibility.Collapsed;
                editLocation.Visibility = Visibility.Collapsed;
                editName.Visibility = Visibility.Collapsed;
            }

            DateTime age = UnixTimeToDate(active.birthdate);

            years.Text = (CalculateAge(age).ToString());

            city.Text = active.location;
            citySelection.SelectedItem = active.location;
            accountText.Text = active.about;
            bioText.Text = active.about;
            name.Text = active.realName;
            showName.Text = active.realName;
            if (active.hobbies != null)
            {
                foreach (var item in active.hobbies)
                {
                    LoadHobbyWrapper(item.displayName);
                }
            }
            LoadHobbyWrapper("Test");
            userInView = active;
        
            years.Text = (DateTime.Now.Year - active.birthdate).ToString();


            string pfPic1 = $"https://145.44.233.207/images/users/{active.profilePicture}";
            ProfilePicture1.Fill = new ImageBrush(new BitmapImage(new Uri(pfPic1, UriKind.Absolute)));
            }
        
    

        public int CalculateAge(DateTime dob)
        {
            // Calculate dif between years
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            // Now check the months and days
            if(dob.Month > today.Month)
            {
                age--;
            }
            else if (dob.Day > today.Day)
            {
                age--;
            }


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
            userInView.realName = name.Text;
            showName.Text = name.Text;
            showName.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Collapsed;
            confirmNameChange.Visibility = Visibility.Collapsed;
            denymNameChange.Visibility = Visibility.Collapsed;
            await MatchmakerAPI_Client.SaveUser(userInView);
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
            userInView.location = citySelection.SelectedItem.ToString();
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
            NavigationService.GoBack();
        }
        private void addHobby_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Visible;
            entryHobbies.Visibility = Visibility.Visible;
            addInterests.Visibility = Visibility.Visible;
            listPossibleInterests.Visibility = Visibility.Visible;

            //LoadHobbies();
        }

        //private void LoadHobbies()
        private void btnEditCoverImage_Click(object sender, RoutedEventArgs e) {
            CoverImageSelecter coverImageSelecter = new CoverImageSelecter();
            coverImageSelecter.Show();
            coverImageSelecter.userID = userInView.id;
        }

        private void confirmNewHobbyList(object sender, MouseButtonEventArgs e)
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

        private void LoadHobbyWrapper(string hobby)
        {
            
            Border hobbyBorder = new Border();
            StackPanel stackPanel = new StackPanel();
            TextBlock hobbyText = new TextBlock();
            Canvas canvas = new Canvas();
            Path path = new Path();

            //hobbyBorder.BorderBrush = new SolidColorBrush(Colors.Purple);
            //hobbyBorder.CornerRadius = new CornerRadius(16);
            //hobbyBorder.BorderThickness = new Thickness(0);
            //hobbyBorder.Width = 100;
            //hobbyBorder.Height = 20;


            hobbyBorder.Background = new SolidColorBrush(Colors.Purple);
            hobbyBorder.Child = stackPanel;
            hobbyBorder.CornerRadius = new CornerRadius(16);

            stackPanel.Orientation = Orientation.Horizontal;

            hobbyText.Text = "Dit is een test";
            hobbyText.FontSize = 14;

            path.Data = Geometry.Parse("M10.6517 11.7123L8 9.06066L5.34835 11.7123C5.05667 12.004 4.57937 12.004 4.28769 11.7123C3.99601 11.4206 3.99601 10.9433 4.28769 10.6516L6.93934 8L4.28769 5.34835C3.99601 5.05667 3.99601 4.57937 4.28769 4.28769C4.57937 3.99601 5.05667 3.99601 5.34835 4.28769L8 6.93934L10.6517 4.28769C10.9433 3.99601 11.4206 3.99601 11.7123 4.28769C12.004 4.57937 12.004 5.05667 11.7123 5.34835L9.06066 8L11.7123 10.6516C12.004 10.9433 12.004 11.4206 11.7123 11.7123C11.4206 12.004 10.9433 12.004 10.6517 11.7123Z");
            path.Fill = new SolidColorBrush(Colors.Black);
            path.Opacity = 0.54;

            canvas.Children.Add(path);

            stackPanel.Children.Add(hobbyText);
            stackPanel.Children.Add(canvas);

            HobbyWrapper.Children.Add(hobbyBorder);


        }

        private async void AddInterests_Click(object sender, RoutedEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Collapsed;
            entryHobbies.Visibility = Visibility.Collapsed;
            addInterests.Visibility = Visibility.Collapsed;
            listPossibleInterests.Visibility = Visibility.Collapsed;
            userInView.hobbies = hobbyData;
            await MatchmakerAPI_Client.SaveUser(userInView);
        }


    }
}
