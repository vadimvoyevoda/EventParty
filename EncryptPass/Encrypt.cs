using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptPass
{
    public static class Encrypt
    {
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="pass">password to ecrypted</param>
        /// <param name="salt">salt for encrypted</param>
        /// <returns>new encrypted password</returns>
        public static string GetEncryptedPassword(string pass, string salt)
        {
            string tempPass = CalculateMD5Hash(pass);
            tempPass += salt;

            return CalculateMD5Hash(tempPass);
        }

        /// <summary>
        /// Create new hashed string based on input data
        /// </summary>
        /// <param name="input">data to hash</param>
        /// <returns>new hashed string</returns>
        public static string CalculateMD5Hash(string input)
        {
            //create hash class
            MD5 md5 = MD5.Create();
            //encoding from string to bytes
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            //create hash on bytes
            byte[] hash = md5.ComputeHash(inputBytes);

            //bytes to string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            //return new string data
            return sb.ToString();
        }
    }
}
