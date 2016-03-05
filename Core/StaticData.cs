using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Core
{
    public static class SystemStaticData
    {
        public static class UserDictionary
        {
            public static string Normal = "正常";
            public static string DisableAccount = "封停";
        }

        public static class AdvDictionary
        {
            public static string Normal = "正常";
            public static string Stop = "停止";
            public static string Prepare = "待补充";
            public static string Refuse = "审核未通过";
            public static string Pause = "中止";
        }

        public static class AdvRewardDictionary
        {
            public static string Normal = "正常";
            public static string Stop = "停止";
        }
        public static class SystemIncomeDictionary
        {
            public static string Normal = "正常";
            public static string Stop = "停止";
        }

        public static class RechargeRewardDictionary
        {
            public static string Normal = "正常";
            public static string Stop = "停止";
        }

        public static IEnumerable<SystemStatus> UserStatus { get; set; }
        public static IEnumerable<SystemStatus> AdvStatus { get; set; }
        public static IEnumerable<SystemStatus> AdvRewardStatus { get; set; }
        public static IEnumerable<SystemStatus> SystemIncomeStatus { get; set; }
        public static IEnumerable<SystemStatus> RechargeRewardStatus { get; set; }

        #region 用户状态

        public static int LookUpUserStatusId(string lookupValue)
        {
            var lookobj = UserStatus.FirstOrDefault(p => p.ShortDescription == lookupValue);
            return lookobj == null ? 0 : lookobj.StatusId;
        }

        public static string LookUpUserStatusId(int lookupValue)
        {
            var lookobj = UserStatus.FirstOrDefault(p => p.StatusId == lookupValue);
            return lookobj == null ? "无效" : lookobj.ShortDescription;
        }

        #endregion


        #region 广告状态
        public static int LookUpAdvStatusId(string lookupValue)
        {
            var lookobj = AdvStatus.FirstOrDefault(p => p.ShortDescription == lookupValue);
            return lookobj == null ? 0 : lookobj.StatusId;
        }

        public static string LookUpAdvStatusId(int lookupValue)
        {
            var lookobj = AdvStatus.FirstOrDefault(p => p.StatusId == lookupValue);
            return lookobj == null ? "无效" : lookobj.ShortDescription;
        }
        #endregion

        #region 广告奖励状态
        public static int LookUpAdvRewardStatusId(string lookupValue)
        {
            var lookobj = AdvRewardStatus.FirstOrDefault(p => p.ShortDescription == lookupValue);
            return lookobj == null ? 0 : lookobj.StatusId;
        }

        public static string LookUpAdvRewardStatusId(int lookupValue)
        {
            var lookobj = AdvRewardStatus.FirstOrDefault(p => p.StatusId == lookupValue);
            return lookobj == null ? "无效" : lookobj.ShortDescription;
        }
        #endregion

        #region 系统服务费状态
        public static int LookUpSystemIncomeStatusId(string lookupValue)
        {
            var lookobj = SystemIncomeStatus.FirstOrDefault(p => p.ShortDescription == lookupValue);
            return lookobj == null ? 0 : lookobj.StatusId;
        }

        public static string LookUpSystemIncomeStatusId(int lookupValue)
        {
            var lookobj = SystemIncomeStatus.FirstOrDefault(p => p.StatusId == lookupValue);
            return lookobj == null ? "无效" : lookobj.ShortDescription;
        }
        #endregion

        #region 充值奖励状态
        public static int LookUpRechargeRewardStatusId(string lookupValue)
        {
            var lookobj = RechargeRewardStatus.FirstOrDefault(p => p.ShortDescription == lookupValue);
            return lookobj == null ? 0 : lookobj.StatusId;
        }

        public static string LookUpRechargeRewardStatusId(int lookupValue)
        {
            var lookobj = RechargeRewardStatus.FirstOrDefault(p => p.StatusId == lookupValue);
            return lookobj == null ? "无效" : lookobj.ShortDescription;
        }
        #endregion

    }



    public enum StatusCategory
    {
        User = 1,
        Adv = 2,
        AdvReward = 3,
        SystemIncome = 4,
        RechargeReward = 5
    }

    public enum UserRole
    {
        Admin = 1,
        NormalUser = 2
    }

}
