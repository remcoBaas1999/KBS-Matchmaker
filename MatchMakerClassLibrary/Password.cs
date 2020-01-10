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

        public static string[] HashPassword(string password) {
            //Generate salt
            byte[] salt = GenerateSalt();

            //Convert salt to string
            string saltString = Convert.ToBase64String(salt);

            //Combine password and salt
            string passAndSalt = password + saltString;

            //Hash the combined string
            byte[] hash = GenerateHash(passAndSalt, salt);

            //Convert the hash to string
            string hashString = Convert.ToBase64String(hash);

            //Return the salt (string) and the hash (string)
            string[] hashAndSalt = new string[2];
            hashAndSalt[0] = hashString;
            hashAndSalt[1] = saltString;
            return hashAndSalt;
        }

        //Generate the salt
        private static byte[] GenerateSalt() {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            provider.GetBytes(salt);
            return salt;
        }

        //Generate the hash
        public static byte[] GenerateHash(string passAndSalt, byte[] salt) {
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(passAndSalt, salt, iterations);
            byte[] hash = PBKDF2.GetBytes(hashSize);
            return hash;
        }
    }
}
