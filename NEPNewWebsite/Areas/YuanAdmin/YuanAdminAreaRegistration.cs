using System.Web.Mvc;

namespace NEPNewWebsite.Areas.YuanAdmin
{
    public class YuanAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "YuanAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "YuanAdmin_default",
                "YuanAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}