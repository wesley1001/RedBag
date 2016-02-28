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
        protected MkmEntities db = new MkmEntities();
        protected BaseUserService uService = new BaseUserService();
    }
}