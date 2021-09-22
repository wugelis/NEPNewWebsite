using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YuanNewWebsite.Helpers.HtmlHelpers
{
    /// <summary>
    /// ASP.NET MVC HtmlHelper (補助方法)
    /// </summary>
    public static class TopAnnouncementHtmlHelper
    {
        /// <summary>
        /// 取得 ernet-main.html 檔案內的 TOP 筆數的資料
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static MvcHtmlString GetTopAnnouncements(
            this System.Web.Mvc.HtmlHelper helper, 
            int top)
        {
            string resultHtml = string.Empty;

            HtmlDocument document2 = new HtmlDocument();
            document2.Load(HttpContext.Current.Server.MapPath("/ernet-main.htm").Replace("\\ErNet", ""));
            HtmlNode[] nodes = document2.DocumentNode.SelectNodes("//span[@lang='EN-US']").ToArray();
            int i = 0;
            foreach (HtmlNode item in nodes)
            {
                string tmpResultHtml = item.ParentNode.InnerHtml;
                string tmpItemText = item.InnerText;

                if (tmpItemText.Trim() == string.Empty)
                {
                    //tmpResultHtml += "<br />";
                    //resultHtml += tmpResultHtml;
                    continue;
                }

                if (tmpItemText.Substring(0, 1) == "[")
                {
                    tmpResultHtml = string.Format("<br/><span class=style121>{0}<img src=\"/Photo/ICyajirusiA3.gif\" alt=\"箭頭圖片\" width=23 height=15 id=\"_x0000_i1033\"></span>", tmpResultHtml);
                    i++;
                }

                if (i > top)
                    break;

                resultHtml += string.Format("<p class=MsoNormal>{0}</p>", tmpResultHtml);
            }

            return MvcHtmlString.Create(resultHtml);
        }
    }
}
