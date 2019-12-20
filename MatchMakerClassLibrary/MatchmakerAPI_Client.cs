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

        public static UserData DeserializeUserData(string json)
        {
            return JsonConvert.DeserializeObject<UserData>(json);
        }

        public static AuthData DeserializeAuthData(string json)
        {
            return JsonConvert.DeserializeObject<AuthData>(json);   
        }
        public static List<MessageData> DeserializeMessageData(string json)
        {
            if (json == null)
            {
                return new List<MessageData>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<MessageData>>(json);
            }
        }
        public static string GetUserData(int id)
        {
            return Get($@"https://145.44.233.207/user/get/id={id}");
        }

        public static string GetUserData(string email)
        {
            return Get($@"https://145.44.233.207/user/get/email={email}");
        }

        public static Dictionary<string, string> GetCoverImages()
        {
            var json = Get($@"https://145.44.233.207/images/covers/get/list");
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static Dictionary<string, int> GetUsers() {
            var json = Get($@"https://145.44.233.207/user/get/all");
            return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        }

        public static UserData[] GetMatches(int id) {
            var json = Get($@"https://145.44.233.207/user/get/matches/id={id}");
            return JsonConvert.DeserializeObject<UserData[]>(json);
        }

        public static async Task<AuthData> GetAuthDataAsync(string email)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var uri = $@"https://145.44.233.207/auth/get/email=" + email;
                var response = await client.GetAsync(uri);
                var authData = JsonConvert.DeserializeObject<AuthData>(await response.Content.ReadAsStringAsync());
                return authData;
            }
            catch (Exception)
            {
                return new AuthData();
            }
        }

        public static async Task<string> GetMessageData(string id)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var uri = $@"https://145.44.233.207/messages/get/id={id}";
            var response = await client.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<bool> AuthenticateAsync(string email, string password)
        {
            bool check = false;

            //Retrieve data
            try
            {
                AuthData response = await GetAuthDataAsync(email);
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
                if (hashString == hashRetrievedString)
                {
                    check = true;
                }
                return check;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(GetAuthDataAsync(email));
            }
            return false;
        }
        private static string Get(string uri)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                Console.WriteLine("De server reageerde niet of staat uit.");
                return null;
            }
        }

        public static async Task<bool> PostNewCoverImageDataAsync(CoverImageData coverImageData)
        {
            string uri = @"https://145.44.233.207/user/post/update/images";
            var result = await Post(uri, coverImageData);
            //doe wat met result
            return true;
        }

        public static async Task<bool> PostNewUserDataAsync(UserData newUserData)
        {
            string uri = @"https://145.44.233.207/user/post/new";
            var result = await Post(uri, newUserData);
            //doe wat met result
            return true;
        }

        public static async Task<bool> PostNewMessage(MessageData newMessageData)
        {
            string uri = @"https://145.44.233.207/messages/post/new";
            var result = await Post(uri, newMessageData);
            //do something with result?
            return true;
        }

        private static async Task<string> Post(string uri, object data)
        {

            string result;
            var json = JsonConvert.SerializeObject(data);
            var dataString = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, dataString);

            result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static async Task<bool> SaveUser(UserData data)
        {
            string url = @"https://145.44.233.207/user/post/update";
            await Post(url, data);

            return true;
        }

        public static List<HobbyData> getAllHobbies()
        {
            List<HobbyData> data = JsonConvert.DeserializeObject<List<HobbyData>>(Get(@"https://145.44.233.207/hobbies/get/all"));
            return data;
        }
         
        public static async Task<bool> declineContactRequest(UserData userDenying, UserData requestUser)
        {
            //Remove contact request
            if (userDenying.requestFrom.Contains(int.Parse(requestUser.id))) {
                userDenying.requestFrom.Remove(int.Parse(requestUser.id));
                requestUser.contacts.Remove(requestUser.id.ToString());
            }

            await SaveUser(userDenying);
            await SaveUser(requestUser);
            
            return true;
        }

        public static async Task<bool> ConfirmContactRequest(UserData confirmingUser, UserData requestUser)
        {
            Dictionary<string, bool> contactList;

            if (confirmingUser.contacts == null) {
                contactList = new Dictionary<string, bool>();
                confirmingUser.contacts = contactList;
            }

            //Confirm request and add to contacts
            if (confirmingUser.requestFrom.Contains(int.Parse(requestUser.id)) && requestUser.contacts.Keys.Contains(confirmingUser.id.ToString())) {
                confirmingUser.contacts.Add(requestUser.id.ToString(), true);
                requestUser.contacts[$"{confirmingUser.id}"] = true;
                confirmingUser.requestFrom.Remove(int.Parse(requestUser.id));

                await SaveUser(confirmingUser);
                await SaveUser(requestUser);
            }

            return true;
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
        public List<HobbyData> hobbies { get; set; }
        public string profilePicture { get; set; }
        public string id { get; set; }
        public long birthdate { get; set; }
        public string coverImage { get; set; }
        public List<int> blockedUsers { get; set; }
        public List<int> requestFrom { get; set; }
        public Dictionary<string, bool> contacts { get; set; }
    }
    public class AuthData
    {
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }

    public class HobbyData
    {
        public string displayName { get; set; }
        public List<string> assocHobbies { get; set; }
    }

    public class CoverImageData
    {
        public int userID { get; set; }
        public string imageName { get; set; }
    }
    public class MessageData
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public int Sender { get; set; }
        public long TimeStamp { get; set; }
    }
}
