using System;
using System.Collections.Generic;

namespace MatchmakerAPI
{
	public class UserData {
		public string email { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
		public string realName { get; set; }
		public string about { get; set; }
		public string city { get; set; }
		public List<Hobby> hobbies { get; set; }
		public int[] eventsAtt { get; set; }
		public int[] eventsOrg { get; set; }
		public string profilePicture { get; set; }
		public int[] pictures { get; set; }
		public int[] matches { get; set; }
		public int[] chats { get; set; }
		public int id { get; set; }
		public long birthdate { get; set; }
		public string coverImage { get; set; }
		public List<int> blockedUsers {get;set;}
    }
}
