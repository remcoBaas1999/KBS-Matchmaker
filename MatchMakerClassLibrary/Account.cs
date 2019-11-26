using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace MatchMakerClassLibrary
{
    public  class Account
    {
        //properties
        public bool LoggedIn { get; set; }
        public string Email { get; set; }
        //methods
        public bool LogIn(string password)
        {
            bool login = false;
            try
            {
                login = ValidEmail(Email);
                LoggedIn = login;

                return login;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public bool ValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void LogOut()
        {
            LoggedIn = false;
        }
        public void RequestPasswordChange()
        {

        }
    }
}
