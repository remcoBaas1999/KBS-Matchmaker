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
        public UserProfile(UserData user)
        {
            InitializeComponent();

            editBio.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Collapsed;
            editName.Visibility = Visibility.Collapsed;
            btnEditCoverImage.Visibility = Visibility.Collapsed;
            DateTime now = DateTime.Now;
            DateTime birthdate = new DateTime(user.birthdate);
            int a = now.Year - birthdate.Year;
            years.Text = a.ToString();
            name.Text = user.realName;
            showName.Text = user.realName;
            city.Text = user.city;
            bioText.Text = user.about;
            //foreach (var item in user.hobbies)
            //{
                //add to list of Hobbies in the XAML
            //}

            userInView = user;
        }
        public UserProfile(UserData active, bool userAccount)
        {
            InitializeComponent();

            editBio.Visibility = Visibility.Visible;
            editLocation.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            btnEditCoverImage.Visibility = Visibility.Visible;
            years.Text = (DateTime.Now.Year - active.birthdate).ToString();
            city.Text = active.location;
            accountText.Text = active.about;
            bioText.Text = active.about;
            name.Text = active.realName;
            showName.Text = active.realName;

            userInView = active;
        }

        private void editName_Click(object sender, RoutedEventArgs e)
        {
            showName.Visibility = Visibility.Collapsed;
            editName.Visibility = Visibility.Collapsed;
            name.Visibility = Visibility.Visible;
            confirmNameChange.Visibility = Visibility.Visible;
            denymNameChange.Visibility = Visibility.Visible;
        }

        private void confirmNameChange_Click(object sender, RoutedEventArgs e) {

            userInView.realName = name.Text;
            showName.Text = name.Text;
            showName.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Collapsed;
            confirmNameChange.Visibility = Visibility.Collapsed;
            denymNameChange.Visibility = Visibility.Collapsed;
            //var result = await MatchmakerAPI_Client.SaveUser(userInView);
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
            //var result = await MatchmakerAPI_Client.SaveUser(userInView);

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

        private async void confirmNewLocation_Click(object sender, RoutedEventArgs e)
        {
            citySelection.SelectedValue = city.Text;
            string newCity = citySelection.Text;
            city.Text = newCity;
            userInView.city = newCity;
            confirmNewLocation.Visibility = Visibility.Collapsed;
            denyLocationChange.Visibility = Visibility.Collapsed;
            editLocation.Visibility = Visibility.Visible;
            citySelection.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Visible;
            //var result = await MatchmakerAPI_Client.SaveUser(userInView);
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

        private void btnEditCoverImage_Click(object sender, RoutedEventArgs e) {
            CoverImageSelecter coverImageSelecter = new CoverImageSelecter();
            coverImageSelecter.Show();
            coverImageSelecter.userID = userInView.id;
        }

        private void confirmNewHobbyList(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void denyNewHobbyList(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
