using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerClassLibrary
{
    public  class Account
    {
        //properties
        public bool LoggedIn { get; set; }
        public string Email { get; set; }
        //methods
        public void LogIn(string password)
        {
            bool login = false;
            //Unhash password
            //Insert code for trying to log in
            LoggedIn = login;
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
