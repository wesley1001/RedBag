﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MkmEntities : DbContext
    {
        public MkmEntities()
            : base("name=MkmEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AccountHistory> AccountHistory { get; set; }
        public DbSet<AdvHistory> AdvHistory { get; set; }
        public DbSet<AdvInfo> AdvInfo { get; set; }
        public DbSet<AdvReward> AdvReward { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<AreaPosition> AreaPosition { get; set; }
        public DbSet<HistoryType> HistoryType { get; set; }
        public DbSet<LoginHistory> LoginHistory { get; set; }
        public DbSet<RechargeHistory> RechargeHistory { get; set; }
        public DbSet<RewardType> RewardType { get; set; }
        public DbSet<SystemConfig> SystemConfig { get; set; }
        public DbSet<SystemStatus> SystemStatus { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<SystemStatusCategory> SystemStatusCategory { get; set; }
        public DbSet<RechargeReward> RechargeReward { get; set; }
        public DbSet<SystemIncomeHistory> SystemIncomeHistory { get; set; }
    }
}
