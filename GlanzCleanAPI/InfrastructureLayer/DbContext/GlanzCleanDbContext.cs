using GlanzCleanAPI.CoreLayer.Entities;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using BusinessManagementAPI.InfrastructureLayer.Repositories.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BusinessManagementAPI.CoreLayer.Entities;


namespace GlanzCleanAPI.InfrastructureLayer.DbContext
{
    public class GlanzCleanDbContext: IdentityDbContext<User>
    {
        public GlanzCleanDbContext(DbContextOptions<GlanzCleanDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWork> EmployeeWorks { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
