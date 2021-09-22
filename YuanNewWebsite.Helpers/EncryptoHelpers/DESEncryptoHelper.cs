using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.Helpers.EncryptoHelpers
{
    /// <summary>
    /// 用途：加解密類別
    /// 建立時間：2016/06/24.
    /// 建立者：Gelis
    /// </summary>
    public class DESEncryptoHelper
    {
        private static string _DESCryptoKey = "12345678";
        public DESEncryptoHelper()
        {
        }
        /// <summary>
        /// 加密字串
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string EncryptDES(string original)
        {
            try
            {
                string DESCryptoKey = _DESCryptoKey;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Key = Encoding.ASCII.GetBytes(DESCryptoKey);
                des.IV = Encoding.ASCII.GetBytes(DESCryptoKey);
                byte[] s = Encoding.ASCII.GetBytes(original);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                byte[] CryptBytes = desencrypt.TransformFinalBlock(s, 0, s.Length);
                return Convert.ToBase64String(CryptBytes);
            }
            catch (Exception ex)
            {
                //throw ex;
                return string.Empty;
            }
        }
        /// <summary>
        /// 解密字串
        /// </summary>
        /// <param name="Base64String"></param>
        /// <returns></returns>
        public static string DecryptDES(string Base64String)
        {
            try
            {
                string DESCryptoKey = _DESCryptoKey;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Key = Encoding.ASCII.GetBytes(DESCryptoKey);
                des.IV = Encoding.ASCII.GetBytes(DESCryptoKey);
                byte[] s = Convert.FromBase64CharArray(Base64String.ToCharArray(), 0, Base64String.Length);
                ICryptoTransform desencrypt = des.CreateDecryptor();
                return Encoding.ASCII.GetString(desencrypt.TransformFinalBlock(s, 0, s.Length));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
