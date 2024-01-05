﻿// <auto-generated />
using System;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GlanzCleanAPI.Migrations
{
    [DbContext(typeof(GlanzCleanDbContext))]
    [Migration("20240104152257_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.EmployeeWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("WorkId");

                    b.ToTable("EmployeeWorks");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.Work", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursWorked")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkBreak")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.EmployeeWork", b =>
                {
                    b.HasOne("GlanzCleanAPI.CoreLayer.Entities.Employee", "Employee")
                        .WithMany("Works")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GlanzCleanAPI.CoreLayer.Entities.Work", "Work")
                        .WithMany("EmployeeWork")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.Invoice", b =>
                {
                    b.HasOne("GlanzCleanAPI.CoreLayer.Entities.Work", "Work")
                        .WithMany("Invoices")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Work");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.Employee", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("GlanzCleanAPI.CoreLayer.Entities.Work", b =>
                {
                    b.Navigation("EmployeeWork");

                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
