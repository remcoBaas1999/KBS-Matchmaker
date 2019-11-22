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
using MatchMakerClassLibrary;


namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            // Change brush to base color
            if(LoginErrorText.Children.Count > 0)
            {
                LoginErrorText.Children.RemoveAt(0);
                //LoginErrorText.Children.RemoveAt(1);
            }

            Account account = new Account();
            account.Email = AccountEmail.Text;
            account.LogIn(AccountPassBox.Password);
            if(account.LoggedIn)
            {
                // Go to the user dashboard
                MessageBox.Show("Ingelogd");
            }
            else
            {
                AccountEmail.BorderBrush = Brushes.Red;
                AccountEmail.Foreground = Brushes.Red;
                AccountPassBox.BorderBrush = Brushes.Red;
                AccountPassBox.Foreground = Brushes.Red;

                TextBlock errorMessage = new TextBlock();
                errorMessage.Text = "Invalid email or password";
                errorMessage.Foreground = Brushes.Red;
                // Image for the error message needs to be implemented here.
                
                LoginErrorText.Children.Add(errorMessage);
            }
        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This area is not availlable right now");
        }

        private void ForgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This area is not availlable right now");
        }
    }
}
