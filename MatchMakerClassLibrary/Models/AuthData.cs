using System;

namespace MatchmakerAPI
{
    public class AuthData
    {
        public string email { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
	}
}
