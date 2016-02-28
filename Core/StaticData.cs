using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Core
{
    public static class StaticData
    {
        public static class UserDictionary
        {
            public static string Normal = "正常";
            public static string DisableAccount = "封停";
        }


        public static IEnumerable<SystemStatus> UserStatus { get; set; }


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

    }

    public enum StatusCategory
    {
        User = 1,
        Adv = 2,
        SystemConfig = 3
    }

}
