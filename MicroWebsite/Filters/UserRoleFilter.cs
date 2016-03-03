using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;

namespace MicroWebsite.Filters
{
    public class UserRoleFilter : ActionFilterAttribute
    {

        public UserRole CheckRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var roleId = Convert.ToInt32(filterContext.HttpContext.Session["Role"]);
                if (roleId != (int)CheckRole)
                    filterContext.Result = new RedirectResult("/User/SignIn");
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("/User/SignIn");
                return;
            }

        }
    }
}