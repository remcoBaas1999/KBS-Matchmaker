using System;

namespace MatchmakerAPI
{
    public class Hobby
    {
        public string displayName { get; set; }
		public Hobby[] assocHobbies { get; set; }
    }
}
