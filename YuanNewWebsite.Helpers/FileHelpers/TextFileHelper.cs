using YuanNewWebsite.Helpers.EncryptoHelpers;
using YuanNewWebsite.VO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YuanNewWebsite.Helpers.FileHelpers
{
    /// <summary>
    /// 用途：加解密文字檔案補助類別
    /// 建立日期：2016/06/24
    /// 建立者：Gelis
    /// </summary>
    public class TextFileHelper
    {
        /// <summary>
        /// (私有變數) textLoginFile.dat 完整路徑.
        /// </summary>
        private static string _textLoginFile = HttpContext.Current.Server.MapPath("../../textLoginFile.dat");
        /// <summary>
        /// 說明：檢核 textLoginFile.dat 檔案是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckTextEncryptExists()
        {
            return File.Exists(_textLoginFile);
        }
        /// <summary>
        /// 說明：取得 LoginUserData 的加密檔案
        /// 建立時間：2016/06/24.
        /// </summary>
        /// <returns></returns>
        public static LoginUserData GetLoginDataFromFile()
        {
            lock (_textLoginFile)
            {
                //如果密碼檔不存在，則重新計建立加密的密碼檔案.
                if (!CheckTextEncryptExists())
                {
                    string passTmp = ConfigurationManager.AppSettings["ErNet.Reset"] != null ? ConfigurationManager.AppSettings["ErNet.Reset"] : "ErNet.PassWord"; //未加密的密碼
                    //string encryptPassTmp = DESEncryptoHelper.EncryptDES(passTmp);
                    LoginUserData loginUserData = new LoginUserData()
                    {
                        LoginId = "Admin",
                        Password = passTmp
                    };
                    bool writeIsOk = WriteLoginData2File(loginUserData);
                    loginUserData.Password = DESEncryptoHelper.DecryptDES(loginUserData.Password);
                    return loginUserData;
                }

                FileStream f = new FileStream(_textLoginFile, FileMode.Open, FileAccess.Read);

                try
                {
                    
                    BinaryFormatter formatter = new BinaryFormatter();
                    LoginUserData result = (LoginUserData)formatter.Deserialize(f);
                    result.Password = DESEncryptoHelper.DecryptDES(result.Password);
                    return result;
                }
                finally
                {
                    f.Close();
                }
            }
        }
        /// <summary>
        /// 說明：寫入 LoginUserData 的加密檔案
        /// 建立時間：2016/06/24.
        /// </summary>
        /// <param name="loginUserData"></param>
        /// <returns></returns>
        public static bool WriteLoginData2File(LoginUserData loginUserData)
        {
            lock (loginUserData)
            {
                loginUserData.Password = DESEncryptoHelper.EncryptDES(loginUserData.Password);

                FileStream f = new FileStream(_textLoginFile, FileMode.Create, FileAccess.ReadWrite);
                try
                {

                    BinaryFormatter formatter = new BinaryFormatter();

                    MemoryStream ms = new MemoryStream();
                    formatter.Serialize(ms, loginUserData);

                    BinaryWriter bw = new BinaryWriter(f);
                    byte[] bs = ms.ToArray();
                    try
                    {
                        bw.Write(bs);
                        return true;
                    }
                    finally
                    {
                        //sw.Close();
                        bw.Close();
                    }
                }
                finally
                {
                    f.Close();
                }
            }
        }
    }
}
