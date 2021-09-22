using System.Collections.Generic;
using System.Web;
using YuanNewWebsite.VO;

namespace YuanNewWebsite.Areas.YuanAdmin.Models
{
    /// <summary>
    /// 提供固定空氣汙染源後台管理介面（功能／介面）
    /// </summary>
    public interface IErNetAdminModel
    {
        /// <summary>
        /// 檢核最新公告檔案檔案名稱顯示條件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        AnnouncementUploadVO CheckERNetResult(IEnumerable<HttpPostedFileBase> files);
    }
}