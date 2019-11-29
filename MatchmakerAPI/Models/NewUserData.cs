using System;

namespace MatchmakerAPI
{
    public class NewUserData
    {
        public string email { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
		public string realName { get; set; }
		public int id { get; set; }
		public int birthdate { get; set; }
    }
}
