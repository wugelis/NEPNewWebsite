using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.VO
{
    /// <summary>
    /// 使用者登入相關資訊
    /// </summary>
   [Serializable]
    public class LoginUserData
    {
        /// <summary>
        /// 登入帳號
        /// </summary>
        [Required]
        public string LoginId { get; set; }
        /// <summary>
        /// 登入密碼
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 登入的 Session Id.
        /// </summary>
        public readonly static string LOGIN_USER_INFO = "LoginUserData";

    }
}
