using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchmakerAPI.Models
{
    public class Message
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public int Sender { get; set; }
        public long TimeStamp { get; set; }
    }
}
