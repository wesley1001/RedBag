using System;
using System.Collections.Generic;
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
        public ActionResult UAdvList()
        {
            
            return View();
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
            var rewardIds = rewardStr.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            //calc totalcost
            var averageCount = count/rewardIds.Length;
            decimal totalValue = 0;
            var allAdvReward = db.RewardType.ToList();
            foreach (var id in rewardIds)
            {
                var queryid = int.Parse(id);
                var currentValue = allAdvReward.First(p => p.RewardTypeId == queryid).RewardValue;
                totalValue += averageCount*currentValue;
            }
            var incomeStatusId = SystemStaticData.LookUpSystemIncomeStatusId(SystemStaticData.SystemIncomeDictionary.Normal);
            var inCo = db.SystemConfig.FirstOrDefault(p => p.Status == incomeStatusId);
            totalValue += count*inCo.SystemIncomeValue;
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
            adv.CreateAt = DateTime.Now;
            adv.UserId = this.CurrentLoginUserId;
            adv.Status = SystemStaticData.LookUpAdvStatusId(SystemStaticData.AdvDictionary.Prepare);
            db.AdvInfo.Add(adv);
            db.SaveChanges();
            //save adv reward
            foreach (var id in rewardIds)
            {
                AdvReward reward=new AdvReward();
                reward.RewardTypeId = int.Parse(id);
                reward.AdvId = adv.AdvId;
                db.AdvReward.Add(reward);
            }
            db.SaveChanges();
            saveResult.Status = (int)DataSaveStatus.Success;
            saveResult.Description = "申请成功，请联系管理员进行广告内容制作";
            return new JavaScriptSerializer().Serialize(saveResult);
        }

        public ActionResult ModifyAdv()
        {
            return View();
        }
    }
}
