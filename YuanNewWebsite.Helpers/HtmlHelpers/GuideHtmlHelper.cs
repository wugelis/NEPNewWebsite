using YuanNewWebsite.Helpers.FileHelpers;
using YuanNewWebsite.Helpers.Utils;
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
    /// 疑難排解頁面動態生成Html用的Helper
    /// </summary>
    public static class GuideHtmlHelper
    {
        /// <summary>
        /// 取得疑難排解folder底下的檔案
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static MvcHtmlString GetGuides(this System.Web.Mvc.HtmlHelper helper,string folderName)
        {
            string resultHtml = string.Empty;
            if (!string.IsNullOrEmpty(folderName))
            {
                var result = (from f in Directory.GetFiles(HttpContext.Current.Server.MapPath(@"~/guide/" + folderName))
                              where Path.GetFileName(f) != "FileCreateDate.txt"
                              select new
                             {
                                 ItemType = "檔案",
                                 Path = string.Format(@"{2}guide/{0}/{1}", folderName, HttpContext.Current.Server.UrlPathEncode(new FileInfo(f).Name), Util.getVirtualPath()),
                                 Name = new FileInfo(f).Name,
                                 CreateDate = new FileInfo(f).CreationTime,
                                 LastWriteTime = new FileInfo(f).LastWriteTime
                             }).OrderByDescending(r => r.CreateDate);

                if (result.Count()>0)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        if (i % 2 == 1)
                        {
                            resultHtml += string.Format("<tr><td  width=\"12\" height=\"40\" bgcolor=\"#DEF8C2\"><img src=\"{2}Photo/sq-gold.gif\" alt=\"標頭小圖示\"   width=\"13\" height=\"13\"/></td><td bgcolor=\"#DEF8C2\"><span class=\"style17\"><a href=\"{0}\" target=\"_blank\" class=\"style15\">{1}</a></span></td></tr>", result.ToList()[i].Path, result.ToList()[i].Name,Util.getVirtualPath());
                        }
                        else
                        {
                            resultHtml += string.Format("<tr><td  width=\"12\" height=\"40\" bgcolor=\"#DEF8C2\"><img src=\"{2}Photo/10(1).gif\" alt=\"標頭小圖示\"   width=\"13\" height=\"13\"/></td><td  bgcolor=\"#DEF8C2\"><span class=\"style17\"><a href=\"{0}\" target=\"_blank\" class=\"style15\">{1}</a></span></td></tr>", result.ToList()[i].Path, result.ToList()[i].Name, Util.getVirtualPath());
                        }

                    }
                    DateTextFileHelper dt = new DateTextFileHelper();
                    string mDate = string.Empty;
                    mDate = dt.GetLastCreateDateTxt(HttpContext.Current.Server.MapPath(@"~/guide/" + folderName));
                    if (!string.IsNullOrEmpty(mDate))
                    {
                        resultHtml += string.Format("<tr><td height=\"20\" bgcolor=\"#FFFFFF\">&nbsp;</td><td  bgcolor=\"#FFFFFF\"><div align=\"right\"><span class=\"style19\">資料更新日期:" + mDate + "</span></div></td></tr>");
                    }
                   

                }

            }
            return MvcHtmlString.Create(resultHtml);
        }
    }
}
