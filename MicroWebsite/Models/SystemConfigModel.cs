using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class RewardConfigModel
    {
        public SystemConfig IncomeModel { get; set; }
        public IList<RewardType> Rewards { get; set; }
        public IList<RechargeReward> RechargeRewards { get; set; } 
    }

    

}