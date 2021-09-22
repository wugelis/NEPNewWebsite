using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using YuanNewWebsite.VO;

namespace YuanNewWebsite.Areas.YuanAdmin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ErNetAdminModel : IErNetAdminModel
    {
        public AnnouncementUploadVO CheckERNetResult(IEnumerable<HttpPostedFileBase> files)
        {
            AnnouncementUploadVO result = null;

            if (files.Where(c => c == null)
                .Count() >= 2)
            {
                result = new AnnouncementUploadVO()
                {
                    Message = "請選擇任一檔案！"
                };
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
                            result = new AnnouncementUploadVO()
                            {
                                Message = "最新公告檔案檔案名稱為『ernet-new.htm』，歷史公告檔案名稱為『ernet-main.htm』！"
                            };
                        }
                    }
                }
            }

            return result;
        }
    }
}