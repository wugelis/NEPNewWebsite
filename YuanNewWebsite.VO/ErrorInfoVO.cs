using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanNewWebsite.VO
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorInfoVO
    {
        public Exception ExceptionInfo { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
