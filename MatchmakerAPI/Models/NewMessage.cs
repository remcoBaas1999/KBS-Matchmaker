using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchmakerAPI.Models {
    public class NewMessage {
        public string chat { get; set; }
        public Message content { get; set; }
    }
}
