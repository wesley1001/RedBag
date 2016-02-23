using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class ManagerUserModel:WorkspaceBaseModel
    {
        public ManagerUserModel()
        {
            this.IsAdmin = true;
        }
        public IList<User> Users { get; set; }
        public User NewUser { get; set; }
    }
}