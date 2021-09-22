using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEPNewWebsite.Controllers
{
    /// <summary>
    /// 管理資訊系統 主要 (Controller)
    /// </summary>
    public class ErNetController : Controller
    {
        /// <summary>
        ///  首頁 (Action)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 公告區 (Action)
        /// </summary>
        /// <returns></returns>
        public ActionResult Announcement()
        {
            return View();
        }
        /// <summary>
        /// 新手上路 (Guide)
        /// </summary>
        /// <returns></returns>
        public ActionResult Guide(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                return RedirectToAction("Index");
            }
            ViewData.Model = folderName;
            return View();
        }
        /// <summary>
        /// 下載專區 (Action)
        /// </summary>
        /// <returns></returns>
        public ActionResult Download(string folderName)
        {

            if (string.IsNullOrEmpty(folderName))
            {
                return RedirectToAction("Index");
            }
            ViewData.Model = folderName;
            return View();
        }

        #region 顯示部分公告的 Action (不使用主板頁面)
        /// <summary>
        /// 顯示部分公告的 Action (不使用主板頁面)
        /// </summary>
        /// <returns></returns>
        public ActionResult TopNews(int? top)
        {
            ViewData.Model = top.HasValue ? top.Value : 3; //預設值傳入 3 筆資料.
            return View();
        }
        #endregion

    }
}