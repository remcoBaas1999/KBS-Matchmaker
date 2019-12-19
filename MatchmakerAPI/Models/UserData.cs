using System;
using System.Collections.Generic;

namespace MatchmakerAPI
{
    public class UserData
    {
        public string email { get; set; }
    		public string password { get; set; }
    		public string salt { get; set; }
    		public string realName { get; set; }
    		public string about { get; set; }
    		public string city { get; set; }
    		public List<Hobby> hobbies { get; set; }
    		public string profilePicture { get; set; }
    		public int id { get; set; }
    		public long birthdate { get; set; }
    		public string coverImage { get; set; }
    		public List<int> blockedUsers { get; set; }
        public List<int> incRequest { get; set; }
        public List<int> outRequest { get; set; }
        public List<int> contacts { get; set; }
    }
}
