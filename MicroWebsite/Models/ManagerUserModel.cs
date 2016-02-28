using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class UserListModel
    {
        public IList<User> Users { get; set; }
    }

    
}