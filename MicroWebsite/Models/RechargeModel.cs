using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace MicroWebsite.Models
{
    public class RechargeModel
    {
        public IList<RechargeHistoryList> List { get; set; }
    }

    public class RechargeHistoryList : RechargeHistory
    {
        public string ToAccountName { get; set; }
        public string RechargeRewardName { get; set; }
        public string OperatorName { get; set; }
    }

    public class RechargeAccountModel
    {
        [Required]
        public string RechargeAccount { get; set; }
        [Required, RegularExpression(@"^[1-9]\d*$", ErrorMessage = "冲入的金额必须为整数")]
        public decimal Cash { get; set; }
        public string Remark { get; set; }
    }

}