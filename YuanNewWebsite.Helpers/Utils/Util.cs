using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YuanNewWebsite.Helpers.Utils
{
    /// <summary>
    /// 共用模組
    /// (請不屬於特定邏輯使用的共用方法都放置在此)
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 取得虛擬路徑-應用程式名稱 (ApplicationPath)
        /// </summary>
        /// <returns></returns>
        public static string getVirtualPath()
        {
            string result = "/";
            HttpContext current = HttpContext.Current;
            string appPath = current.Request.ApplicationPath;
            if (appPath != "/")
            {
                result = string.Format("{0}/", appPath);
            }
            return result;
        }
    }
}
