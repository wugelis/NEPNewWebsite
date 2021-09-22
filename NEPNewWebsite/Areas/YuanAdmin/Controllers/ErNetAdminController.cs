using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuanNewWebsite.Areas.YuanAdmin.Models;
using YuanNewWebsite.Helpers;
using YuanNewWebsite.Helpers.FileHelpers;
using YuanNewWebsite.VO;

namespace NEPNewWebsite.Areas.YuanAdmin.Controllers
{
    public class ErNetAdminController : Controller
    {
        private IErNetAdminModel _erNetAdminModel;
        public ErNetAdminController()
        {
            _erNetAdminModel = ErNetAdminModelFactory.Create();
        }
        #region 檢核傳入的帳密與家密文字檔案內的是否相同
        /// <summary>
        /// 檢核傳入的帳密與家密文字檔案內的是否相同
        /// </summary>
        /// <param name="loginUserData"></param>
        /// <returns></returns>
        private bool CheckUserIdPassword(LoginUserData loginUserData)
        {
            LoginUserData currentUserData = TextFileHelper.GetLoginDataFromFile();
            return (loginUserData.LoginId.ToLower() == currentUserData.LoginId.ToLower()
                && loginUserData.Password == currentUserData.Password);
        }
        #endregion

        // GET: YuanAdmin/ErNetAdmin
        [CheckUserSession]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
#if RELEASE
            string ip = Request.UserHostAddress;
            using (StreamWriter file = new StreamWriter(HttpRuntime.AppDomainAppPath + @"ip_list.txt", true))
            {
                file.WriteLine(ip);
            }
            if (!ip.Equals("118.163.26.49")) return View("Close");
#endif
            return View();
        }
        /// <summary>
        /// (Action) 執行登入
        /// </summary>
        /// <param name="loginUserData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginUserData loginUserData)
        {
            if (ModelState.IsValid)
            {
                if (CheckUserIdPassword(loginUserData))
                {
                    LoginUserData currentUserData = TextFileHelper.GetLoginDataFromFile();
                    Session[LoginUserData.LOGIN_USER_INFO] = currentUserData;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Password", "無效的使用者名稱和密碼");
                }
            }
            return View();
        }
        /// <summary>
        /// (Action) Menu 選單使用 PartialView
        /// </summary>
        /// <returns></returns>
        [CheckUserSession]
        public ActionResult Menu()
        {
            return View();
        }
        /// <summary>
        /// (Action) 登出
        /// </summary>
        /// <returns></returns>
        [CheckUserSession]
        public ActionResult Logout()
        {
            Session[LoginUserData.LOGIN_USER_INFO] = null;
            return RedirectToAction("Login");
        }
        /// <summary>
        /// (Action) 修改密碼
        /// </summary>
        /// <returns></returns>
        [CheckUserSession]
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// (Action) 修改密碼 Submit
        /// </summary>
        /// <param name="loginChangePwd"></param>
        /// <returns></returns>
        [CheckUserSession]
        [HttpPost]
        public ActionResult ChangePassword(LoginChangePwd loginChangePwd)
        {
            if (ModelState.IsValid)
            {
                LoginUserData currentUserData = TextFileHelper.GetLoginDataFromFile();

                if (!CheckUserIdPassword(new LoginUserData()
                {
                    LoginId = currentUserData.LoginId,
                    Password = loginChangePwd.OriginalPassword
                }))
                {
                    ModelState.AddModelError("error", "輸入的舊密碼不正確！");
                    return View();
                }
                if (loginChangePwd.NewPassword != loginChangePwd.NewPasswordConfirm)
                {
                    ModelState.AddModelError("error", "確認密碼與新密碼不相同！");
                    return View();
                }

                LoginUserData currentLogin = (LoginUserData)Session[LoginUserData.LOGIN_USER_INFO];
                TextFileHelper.WriteLoginData2File(new LoginUserData() { LoginId = currentLogin.LoginId, Password = loginChangePwd.NewPasswordConfirm });

                return RedirectToAction("Index");
            }
            return View();
        }
        /// <summary>
        /// 操作疑難排解上傳
        /// </summary>
        /// <returns></returns>
        [CheckUserSession]
        public ActionResult GuideUpload()
        {
            return View();
        }
        /// <summary>
        /// (Action) (POST) 操作疑難排解檔案上傳處理
        /// </summary>
        /// <param name="files">多檔上傳的實體 binary 檔案</param>
        /// <param name="guideFiles">Model Binding 的資料</param>
        /// <returns></returns>
        [CheckUserSession]
        [HttpPost]
        public ActionResult GuideUpload(IEnumerable<HttpPostedFileBase> files, GuideFileUploadVO guideFiles)
        {

            if (files.Where(c => c != null).Count() > 0)
            {
                FileIOProcessHelper.SaveFileByFileID(files, guideFiles.File1, "file1", "guide");
                FileIOProcessHelper.SaveFileByFileID(files, guideFiles.File2, "file2", "guide");
                FileIOProcessHelper.SaveFileByFileID(files, guideFiles.File3, "file3", "guide");
                return View(new GuideFileUploadVO() { Message = "上傳完成！" });
            }
            else
            {
                if (!string.IsNullOrEmpty(guideFiles.Text1) || !string.IsNullOrEmpty(guideFiles.Text2) || !string.IsNullOrEmpty(guideFiles.Text3))
                {
                    string DeleteMessage =
                    FileIOProcessHelper.DeleteFileByFileID(guideFiles.Text1, "file1", "guide") +
                    FileIOProcessHelper.DeleteFileByFileID(guideFiles.Text2, "file2", "guide") +
                    FileIOProcessHelper.DeleteFileByFileID(guideFiles.Text3, "file3", "guide");

                    return View(new GuideFileUploadVO() { Message = DeleteMessage });
                }

                return View(new GuideFileUploadVO() { Message = "請選擇任一檔案！" });

            }
        }
        /// <summary>
        /// (Action) (GET) 公告檔案上傳處理
        /// </summary>
        /// <returns></returns>
        [CheckUserSession]
        public ActionResult AnnouncementUpload()
        {
            return View();
        }
        ///// <summary>
        ///// 檢核條件
        ///// </summary>
        ///// <param name="files"></param>
        ///// <returns></returns>
        //public AnnouncementUploadVO CheckERNetResult(IEnumerable<HttpPostedFileBase> files)
        //{
        //    AnnouncementUploadVO result = null;

        //    if (files.Where(c => c == null)
        //        .Count() >= 2)
        //    {
        //        result = new AnnouncementUploadVO()
        //        {
        //            Message = "請選擇任一檔案！"
        //        };
        //    }
        //    else
        //    {
        //        foreach (var f in files)
        //        {
        //            if (f != null)
        //            {
        //                string fileName = Path.GetFileName(f.FileName).ToLower();
        //                if (!(fileName == "ernet-main.htm" || fileName == "ernet-new.htm"))
        //                {
        //                    result = new AnnouncementUploadVO()
        //                    {
        //                        Message = "最新公告檔案檔案名稱為『ernet-new.htm』，歷史公告檔案名稱為『ernet-main.htm』！"
        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}
        /// <summary>
        /// (Action) (POST) 公告檔案上傳處理
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [CheckUserSession]
        [HttpPost]
        public ActionResult AnnouncementUpload(IEnumerable<HttpPostedFileBase> files, AnnouncementUploadVO announcementFile)
        {
            AnnouncementUploadVO announcement = _erNetAdminModel.CheckERNetResult(files);
            if (announcement != null)
            {
                return View(announcement);
            }

            /* 重構至 Models（for Unit Test）
            if (files.Where(c => c == null)
                .Count() >= 2)
            {
                return View(new AnnouncementUploadVO() { Message = "請選擇任一檔案！" });
            }
            else
            {
                foreach (var f in files)
                {
                    if (f != null)
                    {
                        string fileName = Path.GetFileName(f.FileName).ToLower();
                        if (!(fileName == "ernet-main.htm" || fileName == "ernet-new.htm"))
                        {
                            return View(new AnnouncementUploadVO() { Message = "最新公告檔案檔案名稱為『ernet-new.htm』，歷史公告檔案名稱為『ernet-main.htm』！" });
                        }
                    }
                }
            }
            */
            var result = from r in files
                         where r != null
                         && Path.GetFileName(r.FileName).ToLower() == "ernet-main.htm"
                         select r;

            FileIOProcessHelper.SaveFileByFileID(result, "ernet-main.htm");

            var result2 = from r in files
                          where r != null
                          && Path.GetFileName(r.FileName).ToLower() == "ernet-new.htm"
                          select r;

            FileIOProcessHelper.SaveFileByFileID(result2, "ernet-new.htm");

            return View(new AnnouncementUploadVO() { Message = "上傳完成！" });
        }

        /// <summary>
        /// 檔案下載專區上傳
        /// </summary>
        /// <returns></returns>
        [CheckUserSession]
        public ActionResult DownloadUpload()
        {
            return View();
        }

        /// <summary>
        /// (Action) (POST) 檔案下載專區檔案上傳處理
        /// </summary>
        /// <param name="files">多檔上傳的實體 binary 檔案</param>
        /// <param name="docFiles">Model Binding 的資料</param>
        /// <returns></returns>
        [CheckUserSession]
        [HttpPost]
        public ActionResult DownloadUpload(IEnumerable<HttpPostedFileBase> files, DownloadUploadVO docFiles)
        {
            if (files.Where(c => c != null).Count() > 0)
            {
                FileIOProcessHelper.SaveFileByFileID(files, docFiles.File1, "file1", "doc");
                FileIOProcessHelper.SaveFileByFileID(files, docFiles.File2, "file2", "doc");
                FileIOProcessHelper.SaveFileByFileID(files, docFiles.File3, "file3", "doc");
                FileIOProcessHelper.SaveFileByFileID(files, docFiles.File4, "file4", "doc");
                return View(new DownloadUploadVO() { Message = "上傳完成！" });
            }
            else
            {
                if (!string.IsNullOrEmpty(docFiles.Text1) || !string.IsNullOrEmpty(docFiles.Text2) || !string.IsNullOrEmpty(docFiles.Text3) || !string.IsNullOrEmpty(docFiles.Text4))
                {
                    string DeleteMessage =
                    FileIOProcessHelper.DeleteFileByFileID(docFiles.Text1, "file1", "doc") +
                    FileIOProcessHelper.DeleteFileByFileID(docFiles.Text2, "file2", "doc") +
                    FileIOProcessHelper.DeleteFileByFileID(docFiles.Text3, "file3", "doc") +
                    FileIOProcessHelper.DeleteFileByFileID(docFiles.Text4, "file4", "doc");
                    return View(new DownloadUploadVO() { Message = DeleteMessage });
                }

                return View(new DownloadUploadVO() { Message = "請選擇任一檔案！" });

            }

        }
    }
}