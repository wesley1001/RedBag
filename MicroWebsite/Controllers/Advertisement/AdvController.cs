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
            AdvListModel model= new AdvListModel();
            model.Advs = db.AdvInfo.Where(p => p.UserId == this.CurrentLoginUserId).ToList();
            return View(model);
        }

        public ActionResult AdvList()
        {
            return View();
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
