using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.VO
{
    /// <summary>
    /// (VO) 下載專區檔案上傳更新
    /// </summary>
    public class DownloadUploadVO
    {
        /// <summary>
        /// (VO 或 UI) 的網頁用程式名稱
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 系統權限申請
        /// </summary>
        [Display(Name = "系統權限申請")]
        public string File1 { get; set; }
        /// <summary>
        /// 系統操作手冊
        /// </summary>
        [Display(Name = "系統操作手冊")]
        public string File2 { get; set; }

        /// <summary>
        /// 資料檢核原則
        /// </summary>
        [Display(Name = "生煤網路申報")]
        public string File3 { get; set; }

        /// <summary>
        /// 相關宣導資料
        /// </summary>
        [Display(Name = "會議宣導資料")]
        public string File4 { get; set; }

        /// <summary>
        /// 刪除系統權限申請
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// 刪除系統操作手冊
        /// </summary>
        public string Text2 { get; set; }

        /// <summary>
        /// 刪除資料檢核原則
        /// </summary>
        public string Text3 { get; set; }

        /// <summary>
        /// 刪除相關宣導資料
        /// </summary>
        public string Text4 { get; set; }

        /// <summary>
        /// 顯示伺服器端的提示訊息
        /// </summary>
        public string Message { get; set; }
    }
}
