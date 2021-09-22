using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.VO
{
    /// <summary>
    /// 公告檔案上傳更新 VO
    /// </summary>
    public class AnnouncementUploadVO
    {
        /// <summary>
        /// View 的 Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 最新公告檔案
        /// </summary>
        [Display(Name = "最新公告檔案")]
        public string ErNetMainFile { get; set; }
        /// <summary>
        /// 歷史公告檔案
        /// </summary>
        [Display(Name = "歷史公告檔案")]
        public string ErNetNewFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[Display(Name = "系統訊息")]
        public string Message { get; set; }
    }
}
