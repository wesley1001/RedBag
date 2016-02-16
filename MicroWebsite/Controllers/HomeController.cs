using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedBagService.UserService;

namespace MicroWebsite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            BaseUserService uservice=new BaseUserService();
            var user = uservice.LoginUser("", "");
            return View();
        }

    }
}
