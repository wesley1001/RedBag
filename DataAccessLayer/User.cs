//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserId { get; set; }
        public System.DateTime CreateAt { get; set; }
        public Nullable<System.DateTime> LastLoginIn { get; set; }
        public int Status { get; set; }
        public string MobilePhone { get; set; }
        public string Telephone { get; set; }
        public string CompanyName { get; set; }
        public string AccountName { get; set; }
        public string LegalPersonName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
