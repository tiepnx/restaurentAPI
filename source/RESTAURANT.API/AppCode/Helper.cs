using System;
using System.Security.Cryptography;

namespace RESTAURANT.API.AppCode
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static bool CanWriteLogDB(string status)
        {
            bool res = false;
            try
            {
                res = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["WriteLogDBIfSuccess"]);
            }
            catch
            {
                res = false;
            }

            //if (!res && (status == null || status == API.ServiceConstants.STATUS.SUCCESS))
            //    return false;
            return true;
        }
    }
}