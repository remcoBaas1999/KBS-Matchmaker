﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matchmaker {
    public static class User {
        public static int ID { get; set; }
        public static bool wantsNotifications;
        public static bool showInterests;
        public static bool showAttendingActivities;

        public static string email;
        public static bool loggedIn;
    }
}
