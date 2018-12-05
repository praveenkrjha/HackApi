using JDA.Entities;
using JDA.Entities.Request;
using Serilog;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JDA.Common.Helpers
{
    public static class Utilities
    {
        public static string CreateShortGuid()
        {
            Guid guid = Guid.NewGuid();
            return Convert.ToBase64String(guid.ToByteArray());
        }
        public static TokenDetails ValidateSecurityToken(string encryptedToken)
        {
            var tokenDetails = new TokenDetails();
            try
            {
                //Validate data
                if (string.IsNullOrEmpty(encryptedToken))
                {
                    tokenDetails.TokenStatus = TokenStatus.NotProvided;
                }
                else
                {
                    var decrytedToken = Cryptology.DecryptString(encryptedToken.Trim());
                    //Check if the token was decrypted successfully
                    if (string.IsNullOrEmpty(decrytedToken))
                    {
                        tokenDetails.TokenStatus = TokenStatus.Invalid;
                    }
                    else
                    {
                        var tokenData = decrytedToken.Split(new[] { "##" }, StringSplitOptions.None);
                        tokenDetails.UserId = Convert.ToInt32(tokenData[1]);
                        tokenDetails.TokenStatus = (Convert.ToDateTime(tokenData[2]) > DateTime.UtcNow) ? TokenStatus.Valid : TokenStatus.Expired;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception
                tokenDetails.TokenStatus = TokenStatus.Invalid;
                //SystemLogger.WriteError("Exception occured in ValidateSecurityToken method\n" + ex.Message);
            }
            return tokenDetails;
        }
        public static uint First4BytesOfGuid(out Guid guid)
        {
            guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            var first4Bytes = new byte[4];
            Buffer.BlockCopy(bytes, 0, first4Bytes, 0, 4);
            var firstBytesUIntVal = BitConverter.ToUInt32(first4Bytes, 0);

            int len = firstBytesUIntVal.ToString().Length;
            //If the unit value generated from first 4 bytes of GUID has less than 10 digits then generate a new GUID
            //External number for BLE virtual card should be exactly of 10 digits (i.e two sets of 5 digit numbers like xxxxx-xxxxx)
            if (len < 10) { return 0; }
            return firstBytesUIntVal;
        }        
        public static string GetSha256Hash(string text)
        {
            using (var sha256 = SHA256.Create())
            {                
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get the hashed string.
                var strBuilder = new StringBuilder();
                foreach (byte x in hashedBytes)
                {
                    strBuilder.Append(string.Format("{0:x2}", x));
                }
                return strBuilder.ToString();
            }
        }
    }
}
