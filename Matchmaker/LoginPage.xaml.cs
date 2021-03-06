﻿using System;
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
        private List<TextBlock> GeneralError = new List<TextBlock>();

        public LoginPage()
        {
            InitializeComponent();

            TextBlock EmailError = new TextBlock();
            TextBlock PasswordError = new TextBlock();
            TextBlock MainError = new TextBlock();

            MainError.Text = "Invalid email or password.";
            EmailError.Text = "Email is required.";
            PasswordError.Text = "Password is required.";
            MainError.Foreground = Brushes.Red;
            EmailError.Foreground = Brushes.Red;
            PasswordError.Foreground = Brushes.Red;

            GeneralError.Add(MainError);
            GeneralError.Add(PasswordError);
            GeneralError.Add(EmailError);
        }


        // This is the login process
        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationComplete.Text = "";
            // check if there is any error text visible, if so remove these from the elements
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

            try
            {
                //checks if boxes aren't empty
                if (AccountEmail.Text != "" && AccountPassBox.Password != "")
                {
                    //tries to login, if succeeds goes to homePage.
                    if (await MatchmakerAPI_Client.AuthenticateAsync(account.Email, AccountPassBox.Password))
                    {
                        account.LoggedIn = true;

                        try
                        {
                            User.email = account.Email;
                            User.loggedIn = true;

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

                            User.email = "";
                            User.loggedIn = false;
                        }
                    }
                    else
                    {
                        AccountEmail.BorderBrush = Brushes.Red;
                        AccountEmail.Foreground = Brushes.Red;
                        AccountPassBox.BorderBrush = Brushes.Red;
                        AccountPassBox.Foreground = Brushes.Red;
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
                }
                else
                {
                    AccountEmail.BorderBrush = Brushes.Red;
                    AccountEmail.Foreground = Brushes.Red;
                    AccountPassBox.BorderBrush = Brushes.Red;
                    AccountPassBox.Foreground = Brushes.Red;                
                    AccountPassBox.BorderBrush = Brushes.Red;
                    PasswordError.Children.Add(GeneralError[1]);
                    AccountEmail.BorderBrush = Brushes.Red;
                    EmailError.Children.Add(GeneralError[2]);                    
                }
                // Image for the error message needs to be implemented here.

            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine(MatchmakerAPI_Client.AuthenticateAsync(account.Email, AccountPassBox.Password));
            }
        }

        // 
        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            NavigationService.Navigate(registerPage);
            NavigationService.RemoveBackEntry();
        }

        private void ForgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This area is not available right now");
        }

        // Convert a string containing a hexcode to a solidcolorbrush
        // todo: add a regex check to make sure it is a hex code
        private SolidColorBrush HexToBrushes(string hexCode)
        {
            SolidColorBrush brushes = new SolidColorBrush();
            brushes = (SolidColorBrush)(new BrushConverter().ConvertFromString(hexCode));
            return brushes;
        }

        // Check if the text in the textbox is changed.
        private void AccountEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginErrorText.Children.Remove(GeneralError[0]);

            if (AccountEmail.Text == "")
            {
                AccountEmail.BorderBrush = Brushes.Red;
                EmailError.Children.Add(GeneralError[2]);
            }
            else
            {
                if (AccountEmail.Text != "")
                {
                    AccountEmail.BorderBrush = HexToBrushes("#FFBDBBBB");
                    AccountEmail.Foreground = HexToBrushes("#FFBDBBBB");
                }


                if (EmailError.Children.Count > 0)
                {
                    EmailError.Children.Remove(GeneralError[2]);
                }

            }
        }

        // Check if the password in the passwordbox is changed.
        private void AccountPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginErrorText.Children.Remove(GeneralError[0]);

            if (AccountPassBox.Password == "")
            {
                AccountPassBox.BorderBrush = Brushes.Red;
                PasswordError.Children.Add(GeneralError[1]);
            }
            else
            {
                if (AccountPassBox.Password != "")
                {
                    AccountPassBox.BorderBrush = HexToBrushes("#FFBDBBBB");
                    AccountPassBox.Foreground = HexToBrushes("#FFBDBBBB");
                }
                if (PasswordError.Children.Count > 0)
                {
                    PasswordError.Children.Remove(GeneralError[1]);
                }
            }
        }
    }
}
