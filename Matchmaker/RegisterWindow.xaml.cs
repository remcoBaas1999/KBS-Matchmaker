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
using System.Windows.Shapes;

namespace Matchmaker
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        //To Do: Fix Registration
        //Fix Date
        //ErrorMSG Image
        private const int saltSize = 16;
        private const int hashSize = 16;
        private const int iterations = 100000;
        public RegisterWindow()
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
            Window window = new MainWindow();
            this.Close();
            window.Show();
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
            bool tOS = (bool)ToSCheckBox.IsChecked;
            //checks if passwords are equal
            string name = NameField.Text;
            string email = EmailField.Text;
            string pw = PasswordField.Password;
            string pwA = PasswordAgainField.Password;
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
                pWSucceed=true;
            }
            //Regex check
            string regexString = @"(?=\S *[A - Z])(?=\S *[a - z])(?=\S *[@$!% *#?~&\d])[A-Za-z@$!%*#?~&\d]{8,}";
            Regex regex = new Regex(regexString);
            if (regex.IsMatch(pw)) {
                pWRegex = true;
            }
            //checks if all fields are filled in
            if (name!=""&&pw!=""&&email!="")
            {
                noEmptyFields = true;
            }
            //checks for valid email
            if (new EmailAddressAttribute().IsValid(email))
            {
                emailSucceed = true;              
            }
            //checks date UNIMPLEMENTED
            if (1 == 1)
            {
                dateSucceed = true;
            }
            if (pWSucceed&&pWRegex&&noEmptyFields&&emailSucceed&&dateSucceed&&nameSucceed&&tOS)
            {
                //Do Something With User

                

                this.Close();
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
                if (!pWRegex) {
                    errorMSG += "\nYour password is invalid! Password requires 8 characters with at least 1 lowercase character, at least 1 uppercase character and at least 1 number or special character.";
                }
                if (!emailSucceed&&noEmptyFields)
                {
                    errorMSG += "\nYour Email Is Invalid!";
                }
                if (!dateSucceed)
                {
                    errorMSG += "\nYour Date Of Birth Is Invalid!";
                }
                if (!nameSucceed&&noEmptyFields)
                {
                    errorMSG += "\nYour Name Is Invalid!";
                }
                if (!tOS&&noEmptyFields)
                {
                    errorMSG += "\nPlease Accept Our ToS!";
                }
                errorMSG = errorMSG.Substring(1);
                ErrorMessage.Text = errorMSG;
            }
        }
        public static byte[] CreateHash(string input) {
            //Generate random salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            provider.GetBytes(salt);

            //Generate hash
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(input, salt, iterations);
            return PBKDF2.GetBytes(hashSize);
        }
    }
}
