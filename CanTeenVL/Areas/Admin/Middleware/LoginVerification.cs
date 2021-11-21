using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Middleware
{
    public class LoginVerification : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user-id"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Auth/Login");
                return;
            }
        }
    }
    public class LoginVerification1 : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user-idc"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Customer/Login1");
                return;
            }
        }
    }
}