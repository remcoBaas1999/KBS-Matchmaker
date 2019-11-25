using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerClassLibrary
{
    public class Account
    {
        //properties
        public bool LoggedIn { get; set; }
        public string Email { get; set; }

        //methods
        public void LogIn(string password)
        {
            bool login = true;
            //Unhash password
            //Insert code for trying to log in
            if(Email == "" || password == "")
            {
                login = false;
            }
            LoggedIn = login;
        }

		public boolean Authenticate (string password) {
			return MatchmakerAPI_Client.Authenticate(this.email, password)
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
