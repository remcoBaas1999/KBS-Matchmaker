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
    public class Account
    {
        //properties
        public bool LoggedIn { get; set; }
        public string Email { get; set; }

        //methods
        public bool LogIn(string password)
        {
            bool login;
            try
            {
                login = (ValidEmail(Email) && Authenticate(password));
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

		public bool Authenticate (string password) {
            return MatchmakerAPI_Client.Authenticate(this.Email, password);
            //return true;
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
