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
    public partial class HomePage : Page {
        public HomePage() {
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e) {
        }
        private void RefreshButton_MouseDown(object sender, MouseButtonEventArgs e) {
            //Refresh recommended profiles

            //Refresh Profile1
            Profile1Tag.Content = "";
        }
        private void Profile1Picture1_MouseDown(object sender, MouseButtonEventArgs e) { 
            
            //When MouseDown on a users profilepicture
            
            //1. Open new page
            Page page = new Page();
            NavigationService.Navigate(page);

            //2. Open profile and display
            

        }

        private void Profile1BackgroundPicture_MouseDown(object sender, MouseButtonEventArgs e) {
            Profile1Picture1_MouseDown(sender, e);
        }
        public static class activeUser
        {


            public static string name { get; set; } = "Hans Gruber";
            public static string city { get; set; } = "Zwolle";
            public static DateTime Age { get; set; } = new DateTime(1996, 6, 3);
            public static string Bio { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas bibendum dolor semper nibh ullamcorper, eu bibendum diam venenatis. Sed ut mi dui. Praesent nec efficitur neque, sed interdum mauris. Etiam suscipit dui at sem cursus, vel blandit metus tempor. Cras id turpis massa. Praesent pulvinar, velit et vestibulum consequat, sem diam convallis magna, porta ultrices sapien enim non tellus. In egestas, dolor at dictum semper, est nisi porttitor metus, sit amet posuere enim ex non magna. Suspendisse maximus libero ut lectus scelerisque viverra. Nam et tempus tellus. Mauris vel consectetur erat, a dictum eros. ";
        }

        private void Tempt_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Name = "Reinhart Hendriks";
            user.LoggedIn = false;
            user.Age = new DateTime(1994, 6, 3);
            user.city = "New York";
            user.Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Curabitur posuere, nulla eget pulvinar varius, ex diam efficitur turpis, quis gravida quam tortor ac ligula.Vivamus tempus felis a iaculis porta. Aenean posuere convallis varius. Ut faucibus nulla ipsum, in porta velit iaculis tempus. Lorem ipsum dolor sit amet, consectetur adipiscing elit.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Vestibulum iaculis nibh in libero posuere dictum.Nulla tristique tortor fermentum pellentesque ornare. Etiam mollis, diam quis venenatis rutrum, augue diam tincidunt arcu, porttitor congue eros nibh non eros.Duis sodales vel ligula eu placerat. ";
            Page userProfile = new UserProfile();
            NavigationService.Navigate(userProfile);
        }
    }
}
