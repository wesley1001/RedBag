using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class AdvListModel
    {
        public IList<AdvInfo> Advs { get; set; }
    }
}