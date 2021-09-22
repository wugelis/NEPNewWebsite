using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YuanNewWebsite.Helpers.FileHelpers
{
    /// <summary>
    /// 檔案相關 I/O 處理.
    /// 建立時間：2016/7/1 by Gelis.
    /// </summary>
    public class FileIOProcessHelper
    {
        #region (私有方法) 儲存檔案到特定路徑
        /// <summary>
        /// (私有方法) 儲存檔案到特定路徑
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fileName"></param>
        public static void SaveFileByFileID(
            IEnumerable<HttpPostedFileBase> files,
            string fileName)
        {
            SaveFileByFileID(files, fileName, string.Empty, string.Empty);
        }
        /// <summary>
        /// 儲存檔案到特定路徑
        /// </summary>
        /// <param name="files">IEnumerable 的 HttpPostedFileBase</param>
        /// <param name="fileName">上傳的檔案名稱</param>
        /// <param name="folder">上傳的資料夾名稱</param>
        public static void SaveFileByFileID(
            IEnumerable<HttpPostedFileBase> files,
            string fileName,
            string folder,
            string parantFolder)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (Path.GetFileName(file.FileName) == Path.GetFileName(fileName))
                    {
                        var shFileName = Path.GetFileName(file.FileName);
                        HttpContext currentContext = System.Web.HttpContext.Current;
                        var savePath = string.Empty;
                        var txtPath = string.Empty;
                        if (string.IsNullOrEmpty(folder) && string.IsNullOrEmpty(parantFolder))
                        {
                            savePath = currentContext.Server.MapPath(string.Format("~/{0}", shFileName));
                        }
                        else
                        {
                            savePath = currentContext.Server.MapPath(Path.Combine(string.Format("~/{1}/{0}", folder, parantFolder), shFileName));
                            txtPath = currentContext.Server.MapPath(string.Format("~/{1}/{0}", folder, parantFolder));
                        }
                        file.SaveAs(savePath);

                        if(!string.IsNullOrEmpty(txtPath))
                        {
                            DateTextFileHelper dateTxt = new DateTextFileHelper();
                            dateTxt.SetLastCreateDateTxt(txtPath);
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 刪除特定路徑的檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <param name="parantFolder"></param>
        /// <returns>成功與否</returns>
        public static string DeleteFileByFileID(string fileName, string folder, string parantFolder)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                HttpContext currentContext = System.Web.HttpContext.Current;
                var savePath = string.Empty;
                var txtPath = string.Empty;
                if (string.IsNullOrEmpty(folder) && string.IsNullOrEmpty(parantFolder))
                {
                    savePath = currentContext.Server.MapPath(string.Format("~/{0}", fileName));
                }
                else
                {
                    savePath = currentContext.Server.MapPath(Path.Combine(string.Format("~/{1}/{0}", folder, parantFolder), fileName));
                    txtPath = currentContext.Server.MapPath(string.Format("~/{1}/{0}", folder, parantFolder));
                }
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                    if (!string.IsNullOrEmpty(txtPath))
                    {
                        DateTextFileHelper dateTxt = new DateTextFileHelper();
                        dateTxt.SetLastCreateDateTxt(txtPath);
                    }
                    return string.Format("{0} 刪除成功<br/>", fileName);
                }
                       
                return string.Format("{0} 刪除失敗<br/>",fileName) ;
            }
            return "";                                                              
        }

    }
}
