using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class RewardConfigModel
    {
        public IList<RewardType> Rewards { get; set; }
    }
}