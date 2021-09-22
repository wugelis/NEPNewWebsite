using YuanNewWebsite.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YuanNewWebsite.Helpers
{
    /// <summary>
    /// 自訂的 Action Filter 動作過濾器.
    /// 功能：檢核自動導回登入頁.
    /// Create by Gelis at 2016/05/07.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CheckUserSessionAttribute : ActionFilterAttribute
    {
        #region 複寫 Action 執行前 做 Login Session TimeOut 的檢查，若已經 TimeOut 則導回登入頁面.
        /// <summary>
        /// 複寫 Action 執行前 做 Login Session TimeOut 的檢查，若已經 TimeOut 則導回登入頁面.
        /// </summary>
        /// <param name="filterContext">ActionExecuting 提供的執行內容</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            var user = session[LoginUserData.LOGIN_USER_INFO];

            if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession))
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Content("~/YuanAdmin/ErNetAdmin/Login");
                filterContext.Result = new RedirectResult(loginUrl);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        #endregion
    }
}
