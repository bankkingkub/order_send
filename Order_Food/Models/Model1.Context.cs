﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Order_Food.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Order_Food_dbEntities1 : DbContext
    {
        public Order_Food_dbEntities1()
            : base("name=Order_Food_dbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Add_Store> Add_Store { get; set; }
        public virtual DbSet<Add_store_img> Add_store_img { get; set; }
        public virtual DbSet<Address_C> Address_C { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Description> Description { get; set; }
        public virtual DbSet<Food_Picture> Food_Picture { get; set; }
        public virtual DbSet<Get_catagory> Get_catagory { get; set; }
        public virtual DbSet<Get_location> Get_location { get; set; }
        public virtual DbSet<Get_menu> Get_menu { get; set; }
        public virtual DbSet<Get_storename> Get_storename { get; set; }
        public virtual DbSet<Get_time> Get_time { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<user_account> user_account { get; set; }
        public virtual DbSet<View_showing> View_showing { get; set; }
        public virtual DbSet<View_how_show> View_how_show { get; set; }
        public virtual DbSet<Coment_section> Coment_section { get; set; }
    }
}
