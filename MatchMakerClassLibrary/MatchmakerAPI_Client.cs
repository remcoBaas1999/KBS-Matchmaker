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
using System.Net.Http;
using System.Net.Security;

namespace MatchMakerClassLibrary
{
    public static class MatchmakerAPI_Client
    {
        //public static readonly HttpClient client = new HttpClient();
        public static HttpClient client = new HttpClient();

		public static UserData DeserializeUserData(string json) {
			return JsonConvert.DeserializeObject<UserData>(json);
		}

		public static string GetUserData(int id) {
			return Get($@"https://145.44.233.207/user/get/id={id}");
		}

		public static string GetUserData(string email) {
			return Get($@"https://145.44.233.207/user/get/email={email}");
		}

		public static string GetEventData(int id) {
            //return Get($@"https://145.44.233.207/get/event?id={id}");
            return null;
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
            byte[] hash = Password.GenerateHash(passAndSalt, saltRetrieved);

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
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
		    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

		    using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
		    using(Stream stream = response.GetResponseStream())
		    using(StreamReader reader = new StreamReader(stream))
		    {
		        return reader.ReadToEnd();
		    }
		}
        public static async Task<bool> PostNewUserDataAsync(UserData newUserData) {
            string uri = @"https://145.44.233.207/user/post/new";
            var result = await Post(uri, newUserData);
            //doe wat met result
            return true;
        }
        private static async Task<string> Post(string uri, object data) {

            string result;
            var json = JsonConvert.SerializeObject(data);
            var dataString = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, dataString);

            result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }

    public class UserData
    {
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string realName { get; set; }
        public int id { get; set; }
        public long birthdate { get; set; }
    }
}
