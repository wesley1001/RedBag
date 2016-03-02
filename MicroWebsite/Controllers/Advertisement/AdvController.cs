using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult ModifyAdv()
        {
            return View();
        }
    }
}
