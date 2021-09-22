using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YuanNewWebsite.Areas.YuanAdmin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ErNetAdminModelFactory
    {
        private static IErNetAdminModel _erNetAdminModel;
        public static IErNetAdminModel Create()
        {
            if(_erNetAdminModel == null)
            {
                _erNetAdminModel = new ErNetAdminModel();
            }
            return _erNetAdminModel;
        }
    }
}