using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Core;
using DataAccessLayer;
using MicroWebsite.Filters;
using MicroWebsite.Models;

namespace MicroWebsite.Controllers.Advertisement
{
    public class AdvController : BaseController
    {
        [UserRoleFilter(CheckRole = UserRole.NormalUser)]
        public ActionResult UAdvList()
        {
            AdvListModel model = new AdvListModel();
            var query = from adv in db.AdvInfo
                        join a in db.Area on adv.AreaId equals a.AreaId
                        join u in db.User on adv.UserId equals u.UserId
                        select new AdvDisplayModel
                        {
                            AdvId = adv.AdvId,
                            AreaFullName = a.AreaName,
                            AdvTitle = adv.AdvTitle,
                            CostFeeUrl = adv.CostFeeUrl,
                            TotalCount = adv.TotalCount,
                            TotalCash = adv.TotalCash,
                            CreateAt = adv.CreateAt,
                            Status = adv.Status,
                            PublishUserName = u.AccountName,
                            RemainderCash = adv.RemainderCash
                        };
            model.Advs = query.ToList();
            return View(model);

        }

        public ActionResult AdminEditAdv(int advId)
        {
            return View();
        }

        public ActionResult AdvList()
        {
            AdvListModel model = new AdvListModel();
            var query = from adv in db.AdvInfo
                        join a in db.Area on adv.AreaId equals a.AreaId
                        join u in db.User on adv.UserId equals u.UserId
                        select new AdvDisplayModel
                        {
                            AdvId = adv.AdvId,
                            AreaFullName = a.AreaName,
                            AdvTitle = adv.AdvTitle,
                            CostFeeUrl = adv.CostFeeUrl,
                            TotalCount = adv.TotalCount,
                            TotalCash = adv.TotalCash,
                            CreateAt = adv.CreateAt,
                            Status = adv.Status,
                            PublishUserName = u.AccountName,
                            RemainderCash = adv.RemainderCash
                        };
            model.Advs = query.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [UserRoleFilter(CheckRole = UserRole.NormalUser)]
        public string CreatePreAdv(string advTitle, int areaId, string rewardStr, int count)
        {
            WorkspaceDataSaveResult saveResult = new WorkspaceDataSaveResult();
            var rewardIds = rewardStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //calc totalcost
            var averageCount = count / rewardIds.Length;
            decimal totalValue = 0;
            var allAdvReward = db.RewardType.ToList();
            foreach (var id in rewardIds)
            {
                var queryid = int.Parse(id);
                var currentValue = allAdvReward.First(p => p.RewardTypeId == queryid).RewardValue;
                totalValue += averageCount * currentValue;
            }
            var incomeStatusId = SystemStaticData.LookUpSystemIncomeStatusId(SystemStaticData.SystemIncomeDictionary.Normal);
            var inCo = db.SystemConfig.FirstOrDefault(p => p.Status == incomeStatusId);
            totalValue += count * inCo.SystemIncomeValue;
            //检测账户余额是否够
            var userAccount = db.UserAccount.First(p => p.UserId == this.CurrentLoginUserId);
            if (userAccount.AccountBalance < totalValue)
            {
                saveResult.Status = (int)DataSaveStatus.DataValidationError;
                saveResult.Description = "账户余额不足，请联系管理员充值";
                return new JavaScriptSerializer().Serialize(saveResult);
            }
            //sava adv
            AdvInfo adv = new AdvInfo();
            adv.AdvTitle = advTitle;
            adv.AreaId = areaId;
            adv.TotalCount = count;
            adv.TotalCash = totalValue;
            adv.RemainderCash = totalValue;
            adv.CreateAt = DateTime.Now;
            adv.UserId = this.CurrentLoginUserId;
            adv.Status = SystemStaticData.LookUpAdvStatusId(SystemStaticData.AdvDictionary.Prepare);
            db.AdvInfo.Add(adv);
            db.SaveChanges();
            //save adv reward
            foreach (var id in rewardIds)
            {
                AdvReward reward = new AdvReward();
                reward.RewardTypeId = int.Parse(id);
                reward.AdvId = adv.AdvId;
                db.AdvReward.Add(reward);
            }
            //扣款
            userAccount.AccountBalance -= totalValue;
            //记录账户日志
            AccountHistory accountHistory = new AccountHistory();
            accountHistory.CreateAt = DateTime.Now;
            accountHistory.AccountId = userAccount.AccountId;
            accountHistory.ChangeValue = -totalValue;
            accountHistory.ComeFrom = "发布广告";
            db.AccountHistory.Add(accountHistory);
            db.SaveChanges();
            saveResult.Status = (int)DataSaveStatus.Success;
            saveResult.Description = "申请成功，请联系管理员进行广告内容制作";
            return new JavaScriptSerializer().Serialize(saveResult);
        }

        public ActionResult ModifyAdv(int advId)
        {
            var adv = db.AdvInfo.FirstOrDefault(p => p.AdvId == advId);
            if (adv == null)
                return RedirectToAction("AdvList", "Adv");
            var advQuery = from a in db.AdvInfo
                           join ar in db.Area on a.AreaId equals ar.AreaId
                           where a.AdvId == advId
                           select new AdvDisplayModel
                           {
                               AdvTitle = a.AdvTitle,
                               AdvId = a.AdvId,
                               AreaFullName = ar.AreaName,
                               TotalCount = a.TotalCount,
                               TotalCash = a.TotalCash,
                               ContentUrl = a.ContentUrl
                           };
            var query = from ar in db.AdvReward
                        join rt in db.RewardType on ar.RewardTypeId equals rt.RewardTypeId
                        select new AdvRewardModel
                        {
                            AdvId = ar.AdvId,
                            AdvRewardId = ar.AdvRewardId,
                            AdvRewardUrl = ar.AdvRewardUrl,
                            RewardName = rt.ShortDescription,
                            RewardTypeId = ar.RewardTypeId,
                            RewardValue = rt.RewardValue
                        };
            var returnobj = advQuery.First();
            returnobj.Rewards = query.ToList();
            return View(returnobj);
        }

        [HttpPost]
        public ActionResult UpdateAdv()
        {
            var ids = Request.Form["AdvId"];
            int advId = int.Parse(ids);
            var adv = db.AdvInfo.First(p => p.AdvId == advId);
            if (Request.Form["hidRefuse"] != "1")
            {
                foreach (var key in Request.Form.AllKeys)
                {
                    if (key.Contains("rward|"))
                    {
                        var keyInfo = key.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                        AdvReward advReward = new AdvReward();
                        advReward.AdvId = advId;
                        advReward.AdvRewardId = int.Parse(keyInfo[1]);
                        advReward.RewardTypeId = int.Parse(keyInfo[2]);
                        advReward.AdvRewardUrl = Request.Form[key];
                        db.AdvReward.Attach(advReward);
                        db.Entry(advReward).State = EntityState.Modified;
                    }
                }
                
                adv.ContentUrl = Request.Form["ContentUrl"];
                adv.ModifyAt = DateTime.Now;
            }
            else
            {
                var userAccount = db.UserAccount.First(p => p.UserId == adv.UserId);
                //退款
                userAccount.AccountBalance += adv.TotalCash;
                //记录账户日志
                AccountHistory accountHistory = new AccountHistory();
                accountHistory.CreateAt = DateTime.Now;
                accountHistory.AccountId = userAccount.AccountId;
                accountHistory.ChangeValue = adv.TotalCash;
                accountHistory.ComeFrom = "广告审核未通过，退回扣款";
                db.AccountHistory.Add(accountHistory);
                db.UserAccount.Attach(userAccount);
                db.Entry(userAccount).State=EntityState.Modified;
            }
           
            db.SaveChanges();
            return RedirectToAction("AdvList","Adv");
        }

    }
}
