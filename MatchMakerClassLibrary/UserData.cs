using System;
using System.Collections.Generic;

namespace MatchMakerClassLibrary.Models
{
    public class UserData
    {
        public string email { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
		public string realName { get; set; }
		public string about { get; set; }
		public string city { get; set; }
		public List<string> hobbies { get; set; }
		public string[] eventsAtt { get; set; }
		public string[] eventsOrg { get; set; }
		public string profilePicture { get; set; }
		public string[] pictures { get; set; }
		public string[] matches { get; set; }
		public string[] chats { get; set; }
		public int id { get; set; }
        public DateTime birthday { get; set; }
    }
}
