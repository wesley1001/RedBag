//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountHistory
    {
        public int AccountHistoryId { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public int HistoryTypeId { get; set; }
        public string Description { get; set; }
        public decimal ChangeValue { get; set; }
        public System.DateTime CreateAt { get; set; }
    }
}