using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AES
    {
        /// <summary>
        /// Encryption key (32 chars)
        /// </summary>
        public string Key = "";

        /// <summary>
        /// Initialization vector (16 chars)
        /// </summary>
        public string InitVec = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public AES(string key, string initVec)
        {
            Key = key;
            InitVec = initVec;
        }

        /// <summary>
        /// Encrypt given string
        /// </summary>
        public string Encrypt(string s)
        {
            // Convert passed string to byte array
            byte[] data = Encoding.UTF8.GetBytes(s);

            // AES provider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = Encoding.ASCII.GetBytes(Key);
            aes.IV = Encoding.ASCII.GetBytes(InitVec);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            // Crypto transform
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

            // Encrypt data
            string result = Convert.ToBase64String(transform.TransformFinalBlock(data, 0, data.Length));
            transform.Dispose();

            // Return encryped data
            return result;
        }

        /// <summary>
        /// Decrypt given string
        /// </summary>
        public string Decrypt(string s)
        {
            // Convert passed string to byte array
            byte[] data = Convert.FromBase64String(s);

            // AES provider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = Encoding.ASCII.GetBytes(Key);
            aes.IV = Encoding.ASCII.GetBytes(InitVec);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            // Crypto transform
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

            // Encrypt data
            string result = Encoding.UTF8.GetString(transform.TransformFinalBlock(data, 0, data.Length));
            transform.Dispose();

            // Return encrypted data
            return result;
        }

        /// <summary>
        /// Generates AES key from password
        /// </summary>
        public static string GetKey(string hash)
        {
            return hash.Substring(0, 32);
        }

        /// <summary>
        /// Generates AES initialization vector from password
        /// </summary>
        public static string GetInitVec(string hash)
        {
            return hash.Substring(0, 16); ;
        }
    }
}
