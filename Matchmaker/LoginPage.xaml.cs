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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private List<TextBlock> GeneralError = new List<TextBlock>();

        public LoginPage()
        {
            InitializeComponent();

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

       

            private void LoginBtn_Click(object sender, RoutedEventArgs e)
            {
                if (LoginErrorText.Children.Count > 0)
                {

                    LoginErrorText.Children.RemoveAt(0);
                }

                Account account = new Account();
                account.Email = AccountEmail.Text;
                account.LogIn(AccountPassBox.Password);
                if (account.LoggedIn)
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
                        //Uri uri = new Uri("Images/error.png", UriKind.Relative);
                        //errorImage.Source = new BitmapImage(uri);
                        //Image errorImage = new Image();
                        //BitmapImage src = new BitmapImage();
                        //src.BeginInit();
                        //src.UriSource = new Uri((System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "Images/error.png"), UriKind.Absolute);
                        //src.EndInit();
                        //errorImage.Source = src;
                        //errorImage.Stretch = Stretch.Uniform;
                        //errorImage.Height = 16;
                        //LoginErrorText.Children.Add(errorImage);
                        LoginErrorText.Children.Add(GeneralError[0]);
                    }
                    else
                    {
                        AccountPassBox.BorderBrush = Brushes.Red;
                        PasswordError.Children.Add(GeneralError[1]);
                        AccountEmail.BorderBrush = Brushes.Red;
                        EmailError.Children.Add(GeneralError[2]);
                    }


                    // Image for the error message needs to be implemented here.

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

            // Convert a string containing a hexcode to a solidcolorbrush
            // todo: add a regex check to make sure it is a hex code
            private SolidColorBrush HexToBrushes(string hexCode)
            {
                SolidColorBrush brushes = new SolidColorBrush();
                brushes = (SolidColorBrush)(new BrushConverter().ConvertFromString(hexCode));
                return brushes;
            }

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
