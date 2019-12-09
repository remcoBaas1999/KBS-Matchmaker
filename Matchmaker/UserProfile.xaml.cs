﻿using MatchMakerClassLibrary;
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
            
            years.Text = (CalculateAge(UnixTimeToDateTime(user.birthdate))).ToString();
            name.Text = user.realName;
            showName.Text = user.realName;
            city.Text = user.location;
            bioText.Text = user.about;
        }
        public UserProfile(UserData user, bool userAccount)
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

            DateTime age = UnixTimeToDateTime(user.birthdate);

            years.Text = (CalculateAge(age).ToString());
            city.Text = user.location;
            accountText.Text = user.about;
            bioText.Text = activeUser.Bio;
            name.Text = user.realName;
            showName.Text = user.realName;
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

        public DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
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

        private void addHobby_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Window window = new AddHobbies();
            //window.WindowStyle = WindowStyle.None;
            //window.Show();
            AddHobbies.Visibility = Visibility.Visible;
        }

        private void DenyNewHobbies_Click(object sender, RoutedEventArgs e)
        {
            AddHobbies.Visibility = Visibility.Collapsed;
            entryHobbies.Visibility = Visibility.Collapsed;
            addInterests.Visibility = Visibility.Collapsed;
            inputHobbies.Visibility = Visibility.Collapsed;
            svg122.Visibility = Visibility.Collapsed;
            confirmNewHobbies.Visibility = Visibility.Collapsed;
        }

        private void confirmNewHobbies_Click(object sender, RoutedEventArgs e)
        {
            //foreach cb{i} or for all children of listPossibleInterests
            foreach (var item in listPossibleInterests.Children)
            {
                activeUser. item.ToString();
            }
        }

        private void requestSuggestions_Click(object sender, RoutedEventArgs e)
        {
            List<string> listHobbies = new List<string>() { "Dance", "Muziek", "Programmeren" }; //find interests based on string
            for (int i = 0; i < listHobbies.Count(); i++)
            {
                CheckBox cb = new CheckBox();
                cb.Content = listHobbies[i];
                cb.Name = $"cb{i}";
                listPossibleInterests.Children.Add(cb);
            }
        }
    }
}
