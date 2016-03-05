using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroWebsite.Controllers.GetAdv
{
    public class GetAdvController : Controller
    {
        //
        // GET: /GetAdv/

        public ActionResult Index()
        {
            ViewBag.Scope = Request.QueryString["scope"];
            ViewBag.RequestURL = Request.RawUrl;
            return View();
        }

    }
}
