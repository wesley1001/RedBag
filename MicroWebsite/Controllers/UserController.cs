using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using MicroWebsite.Models;
using RedBagService.UserService;

namespace MicroWebsite.Controllers
{
    public class UserController : Controller
    {
        BaseUserService uService=new BaseUserService();

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(SignInModel signIn)
        {
            if (ModelState.IsValid)
            {
                var user = uService.GetUserByUserName(signIn.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName","user not found");
                }
            }
            return View();
        }

    }
}
