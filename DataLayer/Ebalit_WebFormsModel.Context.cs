﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EbalitWebForms.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Ebalit_WebFormsEntities : DbContext
    {
        public Ebalit_WebFormsEntities()
            : base("name=Ebalit_WebFormsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<User> Users { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<BlogTopic> BlogTopics { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<aspnet_Users> aspnet_Users { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<WineAttribute> WineAttributes { get; set; }
        public DbSet<WineToWineAttribute> WineToWineAttributes { get; set; }
        public DbSet<WineConsumation> WineConsumations { get; set; }
    }
}
