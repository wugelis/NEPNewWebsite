using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YuanNewWebsite.Helpers.HtmlHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileDateHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="serverFilePath"></param>
        /// <returns></returns>
        public static MvcHtmlString GetFileLastWriteTime(this HtmlHelper helper, string serverFilePath)
        {
            string lastDate = new FileInfo(HttpContext.Current.Server.MapPath(serverFilePath)).LastWriteTime.ToString("yyyy/MM/dd HH:mm:dd");
            string resultHtml = string.Format("<span class=\"style19\">資料更新日期:" + lastDate + "</span>");
            return MvcHtmlString.Create(resultHtml);
        }
    }
}
