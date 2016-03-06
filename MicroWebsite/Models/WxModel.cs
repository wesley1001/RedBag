using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroWebsite.Models
{
    public class WxModel
    {
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string TimeStamp { get; set; }
        public string NonceStr { get; set; }
        public string Signature { get; set; }
    }
}