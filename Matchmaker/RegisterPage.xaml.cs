using MatchMakerClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
//using Matchmaker.API;

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        //To Do: Fix Registration (DB-Side)
        //DB-Side Email Uniqueness Verification
        private const string toLogin = "Page1.xaml";
        private const int minimumAge = 16;
        private const int saltSize = 16;
        private const int hashSize = 16;
        private const int iterations = 100000;
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void DayOfBirth_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //otherPossibleInputs handles inputs for num0-9, arrow left & right, backspace and delete
            int[] otherPossibleInputs = new int[] { 32, 2, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 23, 25 };
            //the char.isdigit handles the 1-0 inputs
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && !otherPossibleInputs.Contains((int)e.Key))
            {
                e.Handled = true;
            }
        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri(toLogin, UriKind.Relative));
            LoginPage loginPage = new LoginPage();
            NavigationService.Navigate(loginPage);
            NavigationService.RemoveBackEntry();
        }
        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            //bools for checking if account creation succeeds
            bool pWSucceed = false;
            bool pWRegex = false;
            bool noEmptyFields = false;
            bool emailSucceed = false;
            bool dateSucceed = false;
            bool nameSucceed = false;
            bool oldEnough = false;
            bool emailExists = false;
            bool tOS = (bool)ToSCheckBox.IsChecked;
            //strings with inputdata
            string name = NameField.Text;
            string email = EmailField.Text;
            string pw = PasswordField.Password;
            string pwA = PasswordAgainField.Password;
            //datefield extraction
            ComboBoxItem selectedItem = (ComboBoxItem)MonthOfBirth.SelectedItem;
            int dOBM = Int32.Parse(selectedItem.Tag.ToString());
            int dOBY;
            int dOBD;
            if (YearOfBirth.Text == "")
            {
                dOBY = 0;
            }
            else
            {
                dOBY = Int32.Parse(YearOfBirth.Text);
            }
            if (DayOfBirth.Text == "")
            {
                dOBD = 0;
            }
            else
            {
                dOBD = Int32.Parse(DayOfBirth.Text);
            }
            //check name validity
            if (name.Contains(' '))
            {
                string[] nameSplit = name.Split(' ');
                nameSucceed = true;
                foreach (string n in nameSplit)
                {
                    if (n.Length < 2)
                    {
                        nameSucceed = false;
                    }
                }
            }
            if (pw.Equals(pwA))
            {
                pWSucceed = true;
            }
            //Regex check
            string regexString = @"(?=\S*[A-Z])(?=\S*[a-z])(?=\S*[@$!%*#?~&\d])[A-Za-z@$!%*#?~&\d]{8,}";
            Regex regex = new Regex(regexString);
            if (regex.IsMatch(pw))
            {
                pWRegex = true;
            }
            //checks if all fields are filled in
            if (name != "" && pw != "" && email != "" && pwA != "")
            {
                noEmptyFields = true;
            }
            //checks for valid email
            string regexEmailString = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]+$";
            Regex regexEmail = new Regex(regexEmailString);
            if (regexEmail.IsMatch(email))
            {
                emailSucceed = true;
                emailExists = EmailExists(email);
            }
            //checks date

            if (ValidDateChecker(dOBD, dOBM, dOBY))
            {
                dateSucceed = true;
                int currentYear = Int32.Parse(DateTime.Now.ToString("yyyy"));
                int currentMinimumYear = currentYear - minimumAge;
                int currentMonth = Int32.Parse(DateTime.Now.ToString("MM"));
                int currentDay = Int32.Parse(DateTime.Now.ToString("dd"));
                //checks if person is old enough (matches minimal age)
                if (dOBY < currentMinimumYear || (dOBY == currentMinimumYear && ((dOBM == currentMonth && dOBD <= currentDay) || dOBM < currentMonth)))
                {
                    oldEnough = true;
                }
            }
            //check succes
            if (pWSucceed && pWRegex && noEmptyFields && emailSucceed && dateSucceed && nameSucceed && tOS && oldEnough)
            {
                //Make Errorgrid go away
                ErrorGrid.Visibility = Visibility.Collapsed;
                //Do Something With User
                //Close Page
            }
            else
            {
                string errorMSG = "";
                if (!noEmptyFields)
                {
                    errorMSG += " There Are Empty Fields!";
                }
                if (!pWSucceed)
                {
                    errorMSG += "\nYour Passwords Don't Match!";
                }
                if (!pWRegex && noEmptyFields && pWSucceed)
                {
                    errorMSG += "\nYour Password Is Invalid!\nPassword Requires 8 Characters With At Least:" +
                        "\n-1 Lowercase Character" +
                        "\n-1 Uppercase Character " +
                        "\n-1 Number Or Special Character";
                }
                if (!emailSucceed && noEmptyFields)
                {
                    errorMSG += "\nYour Email Is Invalid!";
                }
                else if (emailExists)
                {
                    errorMSG += "\nYour Email Is Already In Use!";
                }
                if (!dateSucceed)
                {
                    errorMSG += "\nYour Date Of Birth Is Invalid!";
                }
                if (dateSucceed && !oldEnough)
                {
                    errorMSG += "\nYou're Too Young To Use This Application!" +
                        $"\n(Minimum Age Is {minimumAge})";
                }
                if (!nameSucceed && noEmptyFields)
                {
                    errorMSG += "\nYour Name Is Invalid!";
                }
                if (!tOS && noEmptyFields)
                {
                    errorMSG += "\nPlease Accept Our ToS!";
                }
                errorMSG = errorMSG.Substring(1);
                ErrorMessage.Text = errorMSG;
                ErrorGrid.Visibility = Visibility.Visible;
            }
        }
        public static byte[] CreateHash(string input)
        {
            //Generate random salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            provider.GetBytes(salt);

            //Generate hash
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(input, salt, iterations);
            return PBKDF2.GetBytes(hashSize);
        }
        public static bool ValidDate(int day, int month, int year)
        {
            int[] days31 = new int[] { 1, 3, 5, 7, 8, 10, 12 };
            //Checks if day fits in month
            if (month == 2)
            {
                if (year % 4 == 0)
                {
                    return (day <= 28 && day > 0);
                }
                else
                {
                    return (day <= 29 && day > 0);
                }
            }
            else if (days31.Contains(month))
            {
                return (day <= 31 && day > 0);
            }
            else
            {
                return (day <= 30 && day > 0);
            }
        }
        public bool ValidDateChecker(int day, int month, int year)
        {
            int currentYear = Int32.Parse(DateTime.Now.ToString("yyyy"));
            int currentMonth = Int32.Parse(DateTime.Now.ToString("MM"));
            int currentDay = Int32.Parse(DateTime.Now.ToString("dd"));
            //returns true if date is in between 1880 and now
            return ValidDate(day, month, year) && !(year <= 1880 || year > currentYear || (currentYear == year && month > currentMonth) || (currentYear == year && currentMonth == month && day > currentDay));
        }
        public bool EmailExists(string email)
        {
            MatchmakerAPI_Client.GetUserData(email);
            //return MatchmakerAPI_Client.DeserializeUserData(MatchmakerAPI_Client.GetUserData(email)).email!=null;            
            return false;
        }
    }
}