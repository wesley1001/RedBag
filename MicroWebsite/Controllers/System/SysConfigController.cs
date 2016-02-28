using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using MicroWebsite.Models;

namespace MicroWebsite.Controllers.System
{
    public class SysConfigController : BaseController
    {
        

        public ActionResult Index()
        {
            RewardConfigModel rewardConfig = new RewardConfigModel();
            var statusId = SystemStaticData.LookUpAdvRewardStatusId(SystemStaticData.AdvRewardDictionary.Normal);
            rewardConfig.Rewards = db.RewardType.Where(p => p.Status == statusId).ToList();
            return View(rewardConfig);
        }

        public ActionResult CreateReward()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateReward(DataAccessLayer.RewardType rewardType)
        {
            if (ModelState.IsValid)
            {
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

    }
}
