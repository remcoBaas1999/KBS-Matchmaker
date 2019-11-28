using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerClassLibrary {
    public static class Password {
        private const int saltSize = 16;
        private const int hashSize = 16;
        private const int iterations = 100000;
        public static void StorePassword(string email, string password) {
            //Generate salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            provider.GetBytes(salt);

            //Convert salt to string
            string saltString = Convert.ToBase64String(salt);

            //Combine password and salt
            string passSalt = password + saltString;

            //Hash the combined string
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(passSalt, salt, iterations);
            byte[] hash = PBKDF2.GetBytes(hashSize);

            //Convert the hash to string
            string hashString = Convert.ToBase64String(hash);

            //Store the salt (string) and the hash (string)
           // MatchmakerAPI_Client.yeetpassword(email, hashString, saltString);
        }
        public static bool CheckPassword(string email, string password)
        {
            bool check = false;

            //Get salt and hash from database using email
            //string saltRetrievedString = MatchmakerAPI_Client.asksalt(email);
            //string hashRetrievedString = MatchmakerAPI_Client.askhash(email);

            //Convert salt to string
            //byte[] saltRetrieved = Convert.FromBase64String(saltRetrievedString);

            //Combine password and salt
            //string passSalt = password + saltRetrievedString;

            //Hash the combined string
            //Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(passSalt, saltRetrieved, iterations);
            //byte[] hash = PBKDF2.GetBytes(hashSize);

            //Convert the hash to string
            //string hashString = Convert.ToBase64String(hash);

            //Compare the new and old hash
            //if (hashString == hashRetrievedString) {
                
            //}
            return check;
        }
    }
}
