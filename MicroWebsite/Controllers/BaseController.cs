using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedBagService.UserService;

namespace MicroWebsite.Controllers
{
    public class BaseController : Controller
    {
        protected BaseUserService uService = new BaseUserService();
    }
}