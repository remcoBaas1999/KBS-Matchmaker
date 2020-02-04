using System;
using System.Collections.Generic;

namespace MatchmakerAPI
{
    public class ClubData
    {
        public string name { get; set; }
		public List<int> members { get; set; }
        public int admin { get; set; }
    }
}
