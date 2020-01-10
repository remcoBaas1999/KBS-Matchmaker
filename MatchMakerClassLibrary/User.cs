using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matchmaker {
    public static class User {
        public static int ID { get; set; }
        public static bool wantsNotifications;
        public static bool showInterests;

        public static string email;
        public static bool loggedIn;

        // Calculate the current age of the user.
        public static int CalculateAge(long _bday)
        {
            DateTime dob = UnixTimeToDate(_bday);
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (today < dob.AddYears(age)) age--;

            return age;
        }

        // Convert the Unixtime to an object of datetime
        private static DateTime UnixTimeToDate(long _bday)
        {

            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime bday = start.AddSeconds(_bday).ToLocalTime();
            return bday;
        }
    }
}
