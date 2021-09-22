using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.VO
{
    /// <summary>
    /// (VO) 操作疑難排解檔案上傳更新
    /// </summary>
    public class GuideFileUploadVO
    {
        /// <summary>
        /// View 的 Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 憑證常見問題
        /// </summary>
        [Display(Name = "憑證常見問題")]
        public string File1 { get; set; }
        /// <summary>
        /// 系統常見操作問題
        /// </summary>
        [Display(Name = "系統常見操作問題")]
        public string File2 { get; set; }
        /// <summary>
        /// 相關元件與安裝檔案
        /// </summary>
        [Display(Name = "憑證元件下載")]
        public string File3 { get; set; }

        /// <summary>
        /// 刪除憑證常見問題
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// 刪除系統常見操作問題
        /// </summary>
        public string Text2 { get; set; }

        /// <summary>
        /// 刪除相關元件與安裝檔案
        /// </summary>
        public string Text3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[Display(Name = "系統訊息")]
        public string Message { get; set; }
    }
}
