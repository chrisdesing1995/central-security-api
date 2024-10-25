
using System.Text;

namespace CentralSecurity.Infrastructure.Common
{
    public static class CommonService
    {
        private static string key = "84509481-3C9E-4FE7-B398-FC7AC553DDAA";

        public static string ConverToEncrypt(string password) {
            if (string.IsNullOrEmpty(password)) return "";
            password += key;
            var passworBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passworBytes); 
        }

        public static string ConverToDecrypt(string encodeData)
        {
            if (string.IsNullOrEmpty(encodeData)) return "";
            var encodeBytes = Convert.FromBase64String(encodeData);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }
    }
}
