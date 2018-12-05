using Serilog;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JDA.Common.Helpers
{
    public static class Cryptology
    {        
        /// <summary>
        /// The salt
        /// </summary>
        private const string Salt = "O*I&U^Y%T$R#E@W!";

        /// <summary>
        /// The password
        /// </summary>
        private const string Password = "A!S@D#F$G%H^J&K*";
        
        public static string EncryptString(string inputString, ILogger logger)
        {
            var encryptedString = "";
            
            // Create the streams used for encryption.
            try
            {
                var algorithm = Aes.Create();
                var key = new Rfc2898DeriveBytes(Password, Encoding.ASCII.GetBytes(Salt));

                algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
                algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var encryptedStream = new CryptoStream(msEncrypt, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(encryptedStream))
                        {
                            //Write all data to the stream.
                            streamWriter.Write(inputString);
                        }
                        encryptedString = Microsoft.AspNetCore.WebUtilities.Base64UrlTextEncoder.Encode(msEncrypt.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            //return encrypted string
            return encryptedString;
        }

        public static string DecryptString(string cipherText)
        {
            var decryptedString = "";
            try
            {
                var algorithm = Aes.Create();
                var key = new Rfc2898DeriveBytes(Password, Encoding.ASCII.GetBytes(Salt));

                algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
                algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);
                // Create the streams used for decryption. 
                using (var msDecrypt = new MemoryStream(Microsoft.AspNetCore.WebUtilities.Base64UrlTextEncoder.Decode(cipherText)))
                {
                    using (var decryptedStream = new CryptoStream(msDecrypt, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(decryptedStream))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            decryptedString = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception
                //logger.Error(ex.Message);
            }
            //return decrypted string
            return decryptedString;
        }
    }    
    
}
