﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using RedBagService.UserService;

namespace MicroWebsite.Controllers
{
    public enum DataSaveStatus
    {
        Success = 1, DataValidationError = 2, SalesforceError = 3, SystemException = 4
    }
    public class WorkspaceDataSaveResult
    {
        public int Status { get; set; }
        public object Data { get; set; }
        public string Description { get; set; }
    }

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

        protected string CurrentLoginName
        {
            get
            {
                if (Session["UserName"] != null)
                {
                    return Session["UserName"].ToString();
                }

                return "";
            }
        }

        protected MkmEntities db = new MkmEntities();
        protected BaseUserService uService = new BaseUserService();


        protected void LogAccountHistory(int accountId,decimal changeValue,string comefrom)
        {
            AccountHistory accountHistory = new AccountHistory();
            accountHistory.CreateAt = DateTime.Now;
            accountHistory.AccountId = accountId;
            accountHistory.ChangeValue = changeValue;
            accountHistory.ComeFrom = comefrom;
            db.AccountHistory.Add(accountHistory);
        }

    }
}