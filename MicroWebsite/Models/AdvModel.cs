using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class AdvListModel
    {
        public IList<AdvDisplayModel> Advs { get; set; }
    }

    public class AdvDisplayModel : AdvInfo
    {
        public string AreaFullName { get; set; }
        public string PublishUserName { get; set; }
    }

}