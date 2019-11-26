using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerClassLibrary
{
    public class User : Account
    //What needs to be done: Dismiss/Confirm Match
    //Update Pictures/Interests
    //ShowUserData
    {
        //properties
        public int Age { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //attributes
        private List<string> interests = new List<string>();
        private List<string> pictures = new List<string>();
        private List<User> matches = new List<User>();
        private List<User> matchesDeclinedOrPending = new List<User>();
        private List<Comment> comments = new List<Comment>();
        //methods
        public User FindPotentialMatch()
        {
            return null;
        }
        public void DismissPotentialMatch(User user)
        {

        }
        public void ConfirmMatch(User user)
        {

        }
        //get
        public List<string> getInterests()
        {
            return interests;
        }
        public List<string> getPictures()
        {
            return pictures;
        }
        //update
        public void UpdateInterests()
        {

        }
        public void UpdatePictures()
        {

        }
    }
}
