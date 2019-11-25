using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerClassLibrary {
    public static class Password {
        private const int saltSize = 32;
        private const int hashSize = 32;
        private const int iterations = 100000;
        public static void StorePassword(string password) {
            //Generate salt
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            provider.GetBytes(salt);

            //Generate hash
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = PBKDF2.GetBytes(hashSize);

            //Convert salt and hash to strings
            string saltString = BitConverter.ToString(salt);
            string hashString = BitConverter.ToString(hash);

            //Store salt and hash in the database
            //...
        }
        public static bool CheckPassword(string email, string password) {
            bool check = false;

            //Get salt and hashed password from database using email
            string saltStringStored = ""; //...
            string hashStringStored = ""; //...

            //Convert salt back to byte array
            byte[] saltStored = Encoding.ASCII.GetBytes(saltStringStored);

            //hash the provided password using the stored salt
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(password, saltStored, iterations);
            byte[] hashProvided = PBKDF2.GetBytes(hashSize);

            //convert hash to string
            string hashStringProvided = BitConverter.ToString(hashProvided);

            //if the hash of the provided password matches the hash of the stored password, return true
            if (hashStringProvided == hashStringStored) {
                check = true;
            }
            return check;
        }
    }
}
