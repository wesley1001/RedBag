using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroWebsite.Models;

namespace MicroWebsite.Controllers.System
{
    public class SysConfigController : Controller
    {
        //
        // GET: /SysConfig/

        public ActionResult Index()
        {
            WorkspaceBaseModel baseModel=new WorkspaceBaseModel();
            baseModel.IsAdmin = true;
            return View(baseModel);
        }

    }
}
