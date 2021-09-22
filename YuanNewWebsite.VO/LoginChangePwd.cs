using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.VO
{
    /// <summary>
    /// 管理者變更密碼
    /// 建立時間：2016/06/27.
    /// </summary>
    public class LoginChangePwd
    {
        /// <summary>
        /// 舊密碼
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "舊密碼")]
        public string OriginalPassword { get; set; }
        /// <summary>
        /// 新密碼
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [RegularExpression(@"^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "至少要有一個0-9的數字;至少要有一個小寫的英文字母(a-z);少要有一個大寫的英文字母(A-Z);與一個特殊符號！")]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; }
        /// <summary>
        /// 確認新密碼
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [RegularExpression(@"^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "至少要有一個0-9的數字;至少要有一個小寫的英文字母(a-z);少要有一個大寫的英文字母(A-Z);與一個特殊符號！")]
        [Display(Name = "確認新密碼")]
        public string NewPasswordConfirm { get; set; }
    }
}
