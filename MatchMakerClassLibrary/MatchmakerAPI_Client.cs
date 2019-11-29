using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MatchMakerClassLibrary;
using System.Net;
using System.IO;
using System.Net.Http;

namespace MatchMakerClassLibrary
{
    public static class MatchmakerAPI_Client
    {
        public static readonly HttpClient client = new HttpClient();
        public static void yeetpassword(string password, string salt) { }

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

			// 1. Retrieve data
			UserData response = DeserializeUserData(GetUserData(email));

			// 2. Deserialize data into UserData object


			// 3. Hash the entered password using the provided salt
			string salt = response.salt;

			// 4. Compare the UserData object to the entered data
			if (password == response.password) {
				// 4a. Return 'true' if the passwords match
				return true;
			}
			else {
				// 4b. Return 'false' if they don't match
				return false;
			}

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
        private static bool PostNewUserData(UserData newUserData)
        {
            string uri = @"";
            string result = Post(uri, newUserData).Result;
            //doe wat met result
            return true;
        }
        private static async Task<string> Post(string uri, object data)
        {
            string result;
            var json = JsonConvert.SerializeObject(data);
            var dataString = new StringContent(json, Encoding.UTF8, "application/json");
            using (client)
            {

                var response = await client.PostAsync(uri, dataString);

                result = response.Content.ReadAsStringAsync().Result;
            }
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
        public int birthdate { get; set; }
    }
}
