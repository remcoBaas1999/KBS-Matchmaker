using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MatchMakerClassLibrary;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace MatchMakerClassLibrary
{
    public static class MatchmakerAPI_Client
    {
        private const int hashSize = 16;
        private const int iterations = 100000;
        public static void yeetpassword(string email, string password, string salt) { }

		public static UserData DeserializeUserData(string json) {
			return JsonConvert.DeserializeObject<UserData>(json);
		}

		public static string GetUserData(int id) {
			return Get($@"http://145.44.233.207:80/get/user?id={id}");
		}

		public static string GetUserData(string email) {
			return Get($@"http://145.44.233.207:80/get/user?e={email}");
		}

		public static string GetEventData(int id) {
			return Get($@"http://145.44.233.207:80/get/event?id={id}");
		}
        public static bool Authenticate(string email, string password) {
            bool check = false;

            //Retrieve data
            UserData response = DeserializeUserData(GetUserData(email));

            //Get salt and hash from database using email
            string saltRetrievedString = response.salt;
            string hashRetrievedString = response.password;

            //Convert salt to string
            byte[] saltRetrieved = Convert.FromBase64String(saltRetrievedString);

            //Combine password and salt
            string passAndSalt = password + saltRetrievedString;

            //Hash the combined string
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(passAndSalt, saltRetrieved, iterations);
            byte[] hash = PBKDF2.GetBytes(hashSize);

            //Convert the hash to string
            string hashString = Convert.ToBase64String(hash);

            //Compare the new and old hash
            if (hashString == hashRetrievedString) {
                check = true;
            }
            return check;
        }
		private static string Get(string uri)
		{
		    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
		    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
		    using(Stream stream = response.GetResponseStream())
		    using(StreamReader reader = new StreamReader(stream))
		    {
		        return reader.ReadToEnd();
		    }
		}
	}

	public class UserData
    {
        public string email { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
		public string realName { get; set; }
		public string about { get; set; }
		public string city { get; set; }
		public string[] hobbies { get; set; }
		public string[] eventsAtt { get; set; }
		public string[] eventsOrg { get; set; }
		public string profilePicture { get; set; }
		public string[] pictures { get; set; }
		public string[] matches { get; set; }
		public string[] chats { get; set; }
		public int id { get; set; }
    }
}
