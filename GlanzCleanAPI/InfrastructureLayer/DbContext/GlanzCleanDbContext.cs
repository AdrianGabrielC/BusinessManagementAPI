using GlanzCleanAPI.CoreLayer.Entities;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;


namespace GlanzCleanAPI.InfrastructureLayer.DbContext
{
    public class GlanzCleanDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public GlanzCleanDbContext(DbContextOptions<GlanzCleanDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWork> EmployeeWorks { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
