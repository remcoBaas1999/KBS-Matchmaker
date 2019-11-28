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

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    /// 

    

    public partial class UserProfile : Page
    {
        //needs to get the currently active account 
        public User account = new User();
        public UserProfile()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;
            var a = now.Year - Account.birthdate.Year;
            age.Text = a.ToString();
            city.Text = Account.location;
            accountText.Text = Account.bio;
            bioText.Text = Account.bio;
            name.Text = Account.firstName + " " + Account.lastName;
            showName.Text = Account.firstName + " " + Account.lastName; ;
        }
        public void EditBio(string bio)
        {
            account.Bio = bio;
        }

        public void changeFirstName(string name)
        {
            account.FirstName = name;
        }

        public void ChangePostalCode(string code)
        {
            account.PostalCode = code;
        }

        public void ChangeCountry(string country)
        {
            account.Country = country;
        }

        public void ChangeCity(string city)
        {
            account.Country = city;
        }

        public void addtag()
        {
            
        }

        public void removeTag()
        {

        }

        public void addPicture()
        {

        }

        public void removePicture()
        {

        }
        public void uploadPicture()
        {

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
            
            var firstSpaceIndex = name.Text.IndexOf(" ");
            string firstName = name.Text.Substring(0,firstSpaceIndex);
            string lastname = name.Text.Substring(firstSpaceIndex+1);
            Account.firstName = firstName;
            Account.lastName = lastname;
            showName.Text = name.Text;
            showName.Visibility = Visibility.Visible;
            editName.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Collapsed;
            confirmNameChange.Visibility = Visibility.Collapsed;
            denymNameChange.Visibility = Visibility.Collapsed;
            //Save to account in database function
            //close textbox for updated textblock
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
            Account.bio = accountText.Text;
            bioText.Text = accountText.Text;
            denyBioChange.Visibility = Visibility.Collapsed;
            confirmBioChange.Visibility = Visibility.Collapsed;
            accountText.Visibility = Visibility.Collapsed;
            bioText.Visibility = Visibility.Visible;
            editBio.Visibility = Visibility.Visible;
            //Save account text to bio
            
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

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void citySelection_Loaded(object sender, RoutedEventArgs e)
        {
            citySelection.ItemsSource = new List<string> { "Zwolle", "Amsterdam", "Arkhangelsk" };

        }
    }
}
