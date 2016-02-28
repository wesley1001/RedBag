using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using DataAccessLayer;
using MicroWebsite.Filters;
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
                    if (user.Status != SystemStaticData.LookUpUserStatusId(SystemStaticData.UserDictionary.Normal))
                        ModelState.AddModelError("UserName", "该用户以封停");
                    else if (!SecurityUtility.PasswordHash(signIn.Password).Equals(user.Password))
                        ModelState.AddModelError("Password", "用户名或者密码不正确");
                    else
                    {
                        user.LastLoginIn = DateTime.Now;
                        uService.SaveUserSignInLog(user, Request.UserHostAddress);
                        Session["UserId"] = user.UserId;
                        if (user.IsAdmin)
                        {
                            Session["Role"] = (int)UserRole.Admin;
                            return RedirectToAction("UserList", "User");
                        }
                        else
                        {
                            Session["Role"] = (int)UserRole.NormalUser;
                            return RedirectToAction("UAdvList", "Adv");
                        }
                    }
                }
            }
            return View();
        }

        [UserRoleFilter]
        public ActionResult UserList()
        {
            var model = new UserListModel();
            model.Users = db.User.Where(p => !p.IsAdmin).OrderByDescending(p => p.CreateAt).ToList();
            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DataAccessLayer.User user)
        {
            if (ModelState.IsValid)
            {
                user.CreateAt = DateTime.Now;
                user.IsAdmin = false;
                user.Password = SecurityUtility.PasswordHash(user.Password);
                user.Status = SystemStaticData.LookUpUserStatusId(SystemStaticData.UserDictionary.Normal);
                db.User.Add(user);
                db.SaveChanges();
                var account = new UserAccount { UserId = user.UserId };
                db.UserAccount.Add(account);
                db.SaveChanges();
            }
            return RedirectToAction("UserList", "User");
        }

        public ActionResult DisableUserAccount(int userId)
        {
            var user = db.User.FirstOrDefault(p => p.UserId == userId);
            if (user == null)
            {
                return RedirectToAction("UserList", "User");
            }
            user.Status = SystemStaticData.LookUpUserStatusId(SystemStaticData.UserDictionary.DisableAccount);
            db.SaveChanges();
            return RedirectToAction("UserList", "User");
        }

        public ActionResult EnableUserAccount(int userId)
        {
            var user = db.User.FirstOrDefault(p => p.UserId == userId);
            if (user == null)
            {
                return RedirectToAction("UserList", "User");
            }
            user.Status = SystemStaticData.LookUpUserStatusId(SystemStaticData.UserDictionary.Normal);
            db.SaveChanges();
            return RedirectToAction("UserList", "User");
        }

        public ActionResult ResetPassword(int userId)
        {
            var user = db.User.FirstOrDefault(p => p.UserId == userId);
            if (user == null)
            {
                return RedirectToAction("UserList", "User");
            }
            var defaultPwd = ConfigurationManager.AppSettings["DefaultResetPwd"];
            user.Password = SecurityUtility.PasswordHash(defaultPwd);
            db.SaveChanges();
            return RedirectToAction("UserList", "User");
        }

    }
}
