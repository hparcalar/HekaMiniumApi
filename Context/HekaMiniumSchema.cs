using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context {
    public class HekaMiniumSchema : DbContext, IDisposable{
        public DbSet<Plant> Plant { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ItemGroup> ItemGroup { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<BrandModel> BrandModel { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<UnitType> UnitType { get; set; }
        public DbSet<ItemUnit> ItemUnit { get; set; }
        public DbSet<FirmCategory> FirmCategory { get; set; }
        public DbSet<AddressInfo> AddressInfo { get; set; }
        public DbSet<Firm> Firm { get; set; }
        public DbSet<FirmAuthor> FirmAuthor { get; set; }
        public DbSet<Forex> Forex { get; set; }
        public DbSet<ItemReceipt> ItemReceipt { get; set; }
        public DbSet<ItemReceiptDetail> ItemReceiptDetail { get; set; }
        public DbSet<ItemOrder> ItemOrder { get; set; }
        public DbSet<ItemOrderDetail> ItemOrderDetail { get; set; }
        public DbSet<ItemOrderConsume> ItemOrderConsume { get; set; }
        public DbSet<ItemReceiptConsume> ItemReceiptConsume { get; set; }
        public DbSet<Process> Process { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<ProductionLine> ProductionLine { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectCategory> ProjectCategory { get; set; }
        public DbSet<ProjectPhaseTemplate> ProjectPhaseTemplate { get; set; }
        public DbSet<ProjectPhaseTemplateDetail> ProjectPhaseTemplateDetail { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<ProjectCostItem> ProjectCostItem { get; set; }
        public DbSet<ProjectPhase> ProjectPhase { get; set; }
        public DbSet<ProjectTask> ProjectTask { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<UserTeamMember> UserTeamMember { get; set; }
        public DbSet<SysRoleSection> SysRoleSection { get; set; }

        public HekaMiniumSchema() : base(){}
        public HekaMiniumSchema(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options){}

        public new void Dispose() {
            base.Dispose();
        }
    }
}