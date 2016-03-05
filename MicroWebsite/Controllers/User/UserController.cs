using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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
                        Session["UserName"] = user.AccountName;
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

        [UserRoleFilter(CheckRole = UserRole.Admin)]
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

        public ActionResult AdminRechargeHistory()
        {
            RechargeModel model = new RechargeModel();
            var query = from h in db.RechargeHistory
                        join u in db.User on h.UserId equals u.UserId
                        join rt in db.RechargeReward on h.RechargeRewardTypeId equals rt.RechargeRewardId
                        into rewardObj
                        from r in rewardObj.DefaultIfEmpty()
                        join uc in db.User on h.CreateUserId equals uc.UserId
                        select new RechargeHistoryList
                        {
                            RechargeHistoryId = h.RechargeHistoryId,
                            Details = h.Details,
                            CreateAt = h.CreateAt,
                            RechargeCash = h.RechargeCash,
                            ToAccountName = u.AccountName,
                            OperatorName = uc.AccountName,
                            RechargeRewardName = r.RewardValue.ToString()
                        };
            model.List = query.ToList();
            return View(model);
        }

        public ActionResult RechargeAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RechargeAccount(RechargeAccountModel model)
        {
            //find account
            var userAccount =
                db.User.FirstOrDefault(
                    p => p.AccountName == model.RechargeAccount || p.MobilePhone == model.RechargeAccount);
            if (userAccount == null)
            {
                ModelState.AddModelError("RechargeAccount", "帐户不存在，请核对帐户");
                return View(model);
            }
                
            if (ModelState.IsValid)
            {
                var rechargeStatusId = SystemStaticData.LookUpRechargeRewardStatusId(SystemStaticData.RechargeRewardDictionary.Normal);
                DataAccessLayer.RechargeHistory history = new DataAccessLayer.RechargeHistory();
                history.CreateAt = DateTime.Now;
                history.CreateUserId = this.CurrentLoginUserId;
                history.Details = model.Remark;
                history.RechargeCash = model.Cash;
                var reward = db.RechargeReward.OrderByDescending(p => p.TargetValue)
                    .FirstOrDefault(m => m.TargetValue <= model.Cash && m.Status == rechargeStatusId);
                history.RechargeRewardTypeId = reward == null ? 0 : reward.RechargeRewardId;
                history.UserId = userAccount.UserId;
                //账户余额增加
                UserAccount account = db.UserAccount.First(p => p.UserId == userAccount.UserId);
                //记录账户历史记录
                AccountHistory accountHistory = new AccountHistory();
                accountHistory.CreateAt = DateTime.Now;
                accountHistory.AccountId = account.AccountId;
                accountHistory.ChangeValue = model.Cash;
                accountHistory.ComeFrom = "账户充值";
                account.AccountBalance += model.Cash;
                if (reward != null)
                {
                    account.AccountBalance += reward.RewardValue;
                    accountHistory.ChangeValue += reward.RewardValue;
                    accountHistory.Description = "账户充值获得奖励 "+ reward.RewardValue;
                }
                db.RechargeHistory.Add(history);
                db.AccountHistory.Add(accountHistory);
                db.SaveChanges();
                if (reward != null)
                {
                    SystemIncomeHistory incomeHistory = new SystemIncomeHistory();
                    incomeHistory.CreateAt = DateTime.Now;
                    incomeHistory.ComeFrom = "充值奖励";
                    incomeHistory.IncomeValue = -reward.RewardValue;
                    incomeHistory.SomeId = history.RechargeHistoryId;
                    db.SystemIncomeHistory.Add(incomeHistory);
                    db.SaveChanges();
                }
                return Content("<script>alert('充值成功');window.location.href=window.location.href</script>");
            }
            return View(model);
        }

    }
}
