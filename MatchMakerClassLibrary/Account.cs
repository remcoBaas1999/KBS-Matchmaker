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

        public string FirstName { get; set; }

        //methods
        public async Task<bool> LogInAsync(string password)
        {
            
            return await MatchmakerAPI_Client.AuthenticateAsync(this.Email, password);
            
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

		public async Task<bool> Authenticate (string password) {
            return await MatchmakerAPI_Client.AuthenticateAsync(this.Email, password);
            //return true;
		}

        public void LogOut() => LoggedIn = false;
    }

    
}
