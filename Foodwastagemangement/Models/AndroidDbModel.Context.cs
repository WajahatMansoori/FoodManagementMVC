﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Foodwastagemangement.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AndroidDBEntities : DbContext
    {
        public AndroidDBEntities()
            : base("name=AndroidDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<Event_Creator> Event_Creator { get; set; }
        public virtual DbSet<Invitation_Member> Invitation_Member { get; set; }
        public virtual DbSet<Menu_Category> Menu_Category { get; set; }
        public virtual DbSet<Menu_Selection> Menu_Selection { get; set; }
    }
}