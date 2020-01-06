using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchmakerAPI.Models
{
    public class Message
    {
        public string text { get; set; }
        public long timestamp { get; set; }
        public int sender { get; set; }
        public bool seen { get; set; }
    }
}
