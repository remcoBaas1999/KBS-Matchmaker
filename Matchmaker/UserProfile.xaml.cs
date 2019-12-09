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
        //needs to get the currently active account 
        public UserProfile(UserData user)
        {
            InitializeComponent();

            editBio.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Collapsed;
            editName.Visibility = Visibility.Collapsed;
            years.Text = CalculateBirthday(UnixTimeToDate(user.birthdate)).ToString();
            name.Text = user.realName;
            showName.Text = user.realName;
            city.Text = user.location;
            bioText.Text = user.about;
        }
        public UserProfile(UserData user, bool userAccount)
        {
            InitializeComponent();

            editBio.Visibility = Visibility.Visible;
            editLocation.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;


            years.Text = CalculateBirthday(UnixTimeToDate(user.birthdate)).ToString();
            city.Text = user.location;
            accountText.Text = user.about;
            bioText.Text = user.about;
            name.Text = user.realName;
            showName.Text = user.realName;
        }

        // Calculate age. Check the difference between the years. Check if the month is correct and the day as well to get the correct age.
        private int CalculateBirthday(DateTime bday)
        {
            DateTime today = DateTime.Today;

            int age = today.Year - bday.Year;
            bool month = true;

            if (today.Month < bday.Month)
            {
                age--;
                month = false;
            }
            if (month && today.Day < bday.Day)
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

        private void confirmNameChange_Click(object sender, RoutedEventArgs e)
        {
            activeUser.name = name.Text;
            showName.Text = name.Text;
            showName.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Collapsed;
            confirmNameChange.Visibility = Visibility.Collapsed;
            denymNameChange.Visibility = Visibility.Collapsed;
            //Save to account in database function
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

        private void confirmBioChange_Click(object sender, RoutedEventArgs e)
        {
            activeUser.Bio = accountText.Text;
            bioText.Text = accountText.Text;
            denyBioChange.Visibility = Visibility.Collapsed;
            confirmBioChange.Visibility = Visibility.Collapsed;
            accountText.Visibility = Visibility.Collapsed;
            bioText.Visibility = Visibility.Visible;
            editBio.Visibility = Visibility.Visible;
            //Save account text to database
            
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

        private void exit_Click(object sender, RoutedEventArgs e)
        {

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

        private void confirmNewLocation_Click(object sender, RoutedEventArgs e)
        {
            citySelection.SelectedValue = city.Text;
            string newCity = citySelection.Text;
            city.Text = newCity;
            activeUser.city = newCity;
            //Save to database
            confirmNewLocation.Visibility = Visibility.Collapsed;
            denyLocationChange.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Visible;
            citySelection.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Visible;
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
    }
}
