using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ModelLayer
{
    public class EncryptPassword
    {
        public static string CreateSalt()
        {

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[20];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            SHA256Managed sha = new SHA256Managed();
            byte[] result = sha.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));
            return Convert.ToBase64String(result);
        }
    }
}