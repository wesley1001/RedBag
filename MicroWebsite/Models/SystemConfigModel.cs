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

    public class AreaIndexModel
    {
        
        public IList<AreaModel> AllAreaList { get; set; } 
        public IList<AreaPointModel> AreaPointList { get; set; }
        public AreaPosition NewAreaPosion { get; set; }
    }

    public class AreaPointModel
    {
        public int AreaPositionId { get; set; }
        public string AreaName { get; set; }
        public string PositionName { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public class AreaModel : Area
    {
        public string AreaFullName { get; set; }
    }

}