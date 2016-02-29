using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using DataAccessLayer;
using MicroWebsite.Models;

namespace MicroWebsite.Controllers.System
{
    public class SysConfigController : BaseController
    {


        public ActionResult Index()
        {
            RewardConfigModel rewardConfig = new RewardConfigModel();
            var statusId = SystemStaticData.LookUpAdvRewardStatusId(SystemStaticData.AdvRewardDictionary.Normal);
            var incomeStatusId = SystemStaticData.LookUpSystemIncomeStatusId(SystemStaticData.SystemIncomeDictionary.Normal);
            var rechargeStatusId = SystemStaticData.LookUpRechargeRewardStatusId(SystemStaticData.RechargeRewardDictionary.Normal);
            rewardConfig.IncomeModel = db.SystemConfig.FirstOrDefault(p => p.Status == incomeStatusId);
            rewardConfig.RechargeRewards = db.RechargeReward.Where(p => p.Status == rechargeStatusId).ToList();
            rewardConfig.Rewards = db.RewardType.Where(p => p.Status == statusId).ToList();
            return View(rewardConfig);
        }

        public ActionResult CreateReward()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateReward(RewardType rewardType)
        {
            if (ModelState.IsValid)
            {
                //判断奖励条件是否已经存在
                var statusId = SystemStaticData.LookUpAdvRewardStatusId(SystemStaticData.AdvRewardDictionary.Normal);
                var existReward =
                    db.RewardType.FirstOrDefault(
                        p => p.RewardValue == rewardType.RewardValue && p.Status == statusId);
                if (existReward != null)
                {
                    ModelState.AddModelError("RewardValue", "该红包奖励金额已经存在");
                    return View(rewardType);
                }
                rewardType.CreateAt = DateTime.Now;
                rewardType.Status = SystemStaticData.LookUpAdvRewardStatusId(SystemStaticData.AdvRewardDictionary.Normal);
                db.RewardType.Add(rewardType);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "SysConfig");
        }

        public ActionResult DeleteRewardType(int rewardTypeId)
        {
            var re = db.RewardType.FirstOrDefault(p => p.RewardTypeId == rewardTypeId);
            if (re == null)
            {
                return RedirectToAction("Index", "SysConfig");
            }
            re.Status = SystemStaticData.LookUpAdvRewardStatusId(SystemStaticData.AdvRewardDictionary.Stop);
            db.SaveChanges();
            return RedirectToAction("Index", "SysConfig");
        }

        public ActionResult SaveSystemIncome(SystemConfig incomeModel)
        {
            if (incomeModel.ConfigId > 0)
            {
                incomeModel.Status = SystemStaticData.LookUpSystemIncomeStatusId(SystemStaticData.SystemIncomeDictionary.Stop);
                db.SystemConfig.Attach(incomeModel);
                db.Entry(incomeModel).State = EntityState.Modified;
            }
            var income = new SystemConfig();
            income.CreateAt = DateTime.Now;
            income.Status = SystemStaticData.LookUpSystemIncomeStatusId(SystemStaticData.SystemIncomeDictionary.Normal);
            income.SystemIncomeValue = incomeModel.SystemIncomeValue;
            db.SystemConfig.Add(income);
            db.SaveChanges();
            return RedirectToAction("Index", "SysConfig");
        }

        public ActionResult CreateRechargeReward()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRechargeReward(RechargeReward rewardType)
        {
            if (ModelState.IsValid)
            {
                //判断奖励条件是否已经存在
                var rechargeStatusId = SystemStaticData.LookUpRechargeRewardStatusId(SystemStaticData.RechargeRewardDictionary.Normal);
                var existReward =
                    db.RechargeReward.FirstOrDefault(
                        p => p.TargetValue == rewardType.TargetValue && p.Status == rechargeStatusId);
                if (existReward != null)
                {
                    ModelState.AddModelError("TargetValue","该奖励规则已经存在");
                    return View(rewardType);
                }
                rewardType.CreateAt = DateTime.Now;
                rewardType.Status = SystemStaticData.LookUpRechargeRewardStatusId(SystemStaticData.RechargeRewardDictionary.Normal);
                db.RechargeReward.Add(rewardType);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "SysConfig");
        }

        public ActionResult DeleteRechargeReward(int rechargeRewardId)
        {
            var re = db.RechargeReward.FirstOrDefault(p => p.RechargeRewardId == rechargeRewardId);
            if (re == null)
            {
                return RedirectToAction("Index", "SysConfig");
            }
            re.Status = SystemStaticData.LookUpRechargeRewardStatusId(SystemStaticData.RechargeRewardDictionary.Stop);
            db.SaveChanges();
            return RedirectToAction("Index", "SysConfig");
        }

    }
}
