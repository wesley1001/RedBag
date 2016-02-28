using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using RedBagService.UserService;

namespace MicroWebsite.Controllers
{
    public class BaseController : Controller
    {
        protected int CurrentLoginUserId
        {
            get
            {
                if (Session["UserId"] != null)
                {
                    return Convert.ToInt32(Session["UserId"]);
                }
                return 0;
            }
        }

        protected MkmEntities db = new MkmEntities();
        protected BaseUserService uService = new BaseUserService();
    }
}