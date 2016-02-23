using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SecurityUtility
    {
        public static string PasswordHash(string password)
        {
            return Get_SHA1(password);
        }

        private static string Get_SHA1(string strSource)
        {
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bytResult = sha.ComputeHash(Encoding.Default.GetBytes(strSource));
            //转换成字符串，32位  
            string strResult = BitConverter.ToString(bytResult);
            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉  
            strResult = strResult.Replace("-", "");
            return strResult;
        }
    }
}
