using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using DataAccessLayer;
using MicroWebsite.Models;
using RedBagService.UserService;

namespace MicroWebsite.Controllers.User
{
    public class UserController : BaseController
    {
        

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
                    ModelState.AddModelError("UserName", "不存在该用户");
                }
                else
                {
                    if (user.Status != StaticData.LookUpUserStatusId("正常"))
                        ModelState.AddModelError("UserName", "该用户以封停");
                    else if (!SecurityUtility.PasswordHash(signIn.Password).Equals(user.Password))
                        ModelState.AddModelError("Password", "用户名或者密码不正确");
                    else
                    {
                        user.LastLoginIn = DateTime.Now;
                        uService.SaveUserSignInLog(user,Request.UserHostAddress);
                    }
                }
            }
            return View();
        }

        public ActionResult CreateAdvUser()
        {
            return null;
        }

        public string TestPwd(string pwd)
        {
            return SecurityUtility.PasswordHash(pwd);
        }

    }
}
