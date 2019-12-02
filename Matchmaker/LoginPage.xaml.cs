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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private List<TextBlock> GeneralError = new List<TextBlock>(); // List of all errors in textblock.

        // Setup everything for the loginpage that is important.
        public LoginPage()
        {
            InitializeComponent();
            TextBlock MainError = new TextBlock
            {
                Text = "Invalid email or password."
            };

            TextBlock EmailError = new TextBlock
            {
                Text = "Email is required."
            };
            TextBlock PasswordError = new TextBlock
            {
                Text = "Password is required."
            };
            MainError.Foreground = Brushes.Red;
            EmailError.Foreground = Brushes.Red;
            PasswordError.Foreground = Brushes.Red;

            GeneralError.Add(MainError);
            GeneralError.Add(PasswordError);
            GeneralError.Add(EmailError);
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginErrorText.Children.Count > 0)
            {
                LoginErrorText.Children.RemoveAt(0);
            }
            if (EmailError.Children.Count > 0)
            {
                EmailError.Children.RemoveAt(0);
            }
            if (PasswordError.Children.Count > 0)
            {
                PasswordError.Children.RemoveAt(0);
            }

            Account account = new Account
            {
                Email = AccountEmail.Text
            };
            account.LogIn(AccountPassBox.Password);
            switch (account.LoggedIn)
            {
                case true:
                    {
                        // Go to the user dashboard
                        //MessageBox.Show("Ingelogd");
                        try
                        {
                            HomePage homePage = new HomePage();
                            NavigationService.Navigate(homePage);
                            NavigationService.RemoveBackEntry();
                        }
                        catch (Exception)
                        {
                            TextBlock LoginTryError = new TextBlock
                            {
                                Text = "Sorry, something went wrong during the process. Please try again.",
                                Foreground = Brushes.Red
                            };
                            LoginErrorText.Children.Add(LoginTryError);
                        }

                        break;
                    }

                default:
                    AccountEmail.BorderBrush = Brushes.Red;
                    AccountEmail.Foreground = Brushes.Red;
                    AccountPassBox.BorderBrush = Brushes.Red;
                    AccountPassBox.Foreground = Brushes.Red;

                    if (AccountEmail.Text != "" && AccountPassBox.Password != "")
                    {
                        if (EmailError.Children.Count > 0)
                        {
                            EmailError.Children.Remove(GeneralError[2]);
                        }
                        if (PasswordError.Children.Count > 0)
                        {
                            PasswordError.Children.Remove(GeneralError[1]);
                        }

                        LoginErrorText.Children.Add(GeneralError[0]);
                    }
                    else
                    {
                        AccountPassBox.BorderBrush = Brushes.Red;
                        PasswordError.Children.Add(GeneralError[1]);
                        AccountEmail.BorderBrush = Brushes.Red;
                        EmailError.Children.Add(GeneralError[2]);
                    }

                    break;
            }
        }

        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegisterPage registerPage = new RegisterPage();
                NavigationService.Navigate(registerPage);
                NavigationService.RemoveBackEntry();
            }
            catch(Exception)
            {
                MessageBox.Show("Sorry, this page is unavaillable.");
            }
        }

        private void ForgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This area is not availlable right now");
        }

        // Convert a string containing a hexcode to a solidcolorbrush
        // todo: add a regex check to make sure it is a hex code
        private SolidColorBrush HexToBrushes(string hexCode)
        {
            SolidColorBrush brushes = new SolidColorBrush();
            brushes = (SolidColorBrush)new BrushConverter().ConvertFromString(hexCode);

            return brushes;
        }

        private void AccountEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginErrorText.Children.Remove(GeneralError[0]);

            switch (AccountEmail.Text)
            {
                case "":
                    AccountEmail.BorderBrush = Brushes.Red;
                    EmailError.Children.Add(GeneralError[2]);
                    break;
                default:
                    if (AccountEmail.Text != "")
                    {
                        AccountEmail.BorderBrush = HexToBrushes("#FFBDBBBB");
                        AccountEmail.Foreground = HexToBrushes("#FFBDBBBB");
                    }


                    if (EmailError.Children.Count > 0)
                    {
                        EmailError.Children.Remove(GeneralError[2]);
                    }

                    break;
            }
        }

        private void AccountPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginErrorText.Children.Remove(GeneralError[0]);

            switch (AccountPassBox.Password)
            {
                case "":
                    AccountPassBox.BorderBrush = Brushes.Red;
                    PasswordError.Children.Add(GeneralError[1]);
                    break;
                default:
                    if (AccountPassBox.Password != "")
                    {
                        AccountPassBox.BorderBrush = HexToBrushes("#FFBDBBBB");
                        AccountPassBox.Foreground = HexToBrushes("#FFBDBBBB");
                    }
                    if (PasswordError.Children.Count > 0)
                    {
                        PasswordError.Children.Remove(GeneralError[1]);
                    }

                    break;
            }
        }
    }
}
