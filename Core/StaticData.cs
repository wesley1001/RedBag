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
        public static IEnumerable<SystemStatus> UserStatus { get; set; }


        public static int LookUpUserStatusId(string lookupValue)
        {
            var lookobj = UserStatus.FirstOrDefault(p => p.ShortDescription == lookupValue);
            return lookobj == null ? 0 : lookobj.StatusId;
        }
    }

    public enum StatusCategory
    {
        User = 1,
        Adv = 2,
        SystemConfig = 3
    }

}
