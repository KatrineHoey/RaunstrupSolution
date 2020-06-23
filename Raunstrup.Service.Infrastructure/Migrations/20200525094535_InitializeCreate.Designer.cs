﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Raunstrup.Service.Infrastructure.Database;

namespace Raunstrup.Service.Infrastructure.Migrations
{
    [DbContext(typeof(RaunstrupContext))]
    [Migration("20200525094535_InitializeCreate")]
    partial class InitializeCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountGroup")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNo")
                        .HasColumnType("int");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cpr")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeNo")
                        .HasColumnType("int");

                    b.Property<bool>("IsProjectleader")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNo")
                        .HasColumnType("int");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<int>("ProfessionRefID")
                        .HasColumnType("int");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.Property<string>("Specialisation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ProfessionRefID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemNo")
                        .HasColumnType("int");

                    b.Property<string>("MeasuringUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.Property<int>("RowVersion")
                        .HasColumnType("int");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountProcent")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("PayForUsedItems")
                        .HasColumnType("bit");

                    b.Property<int?>("ProjectleaderRefId")
                        .HasColumnType("int");

                    b.Property<int>("Rowversion")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPriceWithDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("WorkingTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProjectleaderRefId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferAssignedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("MeasuringUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OfferPricePer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OfferRefId")
                        .HasColumnType("int");

                    b.Property<int>("Rowversion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferRefId");

                    b.ToTable("OfferAssignedItem");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferDriving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeRefId")
                        .HasColumnType("int");

                    b.Property<int>("OfferRefId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rowversion")
                        .HasColumnType("int");

                    b.Property<DateTime>("TodaysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeRefId");

                    b.HasIndex("OfferRefId");

                    b.ToTable("OfferDriving");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferEmployee", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeRefId")
                        .HasColumnType("int");

                    b.Property<int>("OfferRefId")
                        .HasColumnType("int");

                    b.Property<int>("Rowversion")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("EmployeeRefId");

                    b.HasIndex("OfferRefId");

                    b.ToTable("OfferEmployee");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferUsedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeRefId")
                        .HasColumnType("int");

                    b.Property<string>("MeasuringUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OfferPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OfferRefId")
                        .HasColumnType("int");

                    b.Property<int>("Rowversion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeRefId");

                    b.HasIndex("OfferRefId");

                    b.ToTable("OfferUsedItem");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferWorkingHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfWorking")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeRefId")
                        .HasColumnType("int");

                    b.Property<decimal>("HourlyPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OfferRefId")
                        .HasColumnType("int");

                    b.Property<int>("Rowversion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeRefId");

                    b.HasIndex("OfferRefId");

                    b.ToTable("OfferWorkingHours");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Profession", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("HourPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Profession");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Employee", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionRefID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.Offer", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Employee", "Projectleader")
                        .WithMany()
                        .HasForeignKey("ProjectleaderRefId");
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferAssignedItem", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Offer", "Offer")
                        .WithMany("AssignedItems")
                        .HasForeignKey("OfferRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferDriving", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Offer", "Offer")
                        .WithMany("ProjectDrivings")
                        .HasForeignKey("OfferRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferEmployee", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Offer", "Offer")
                        .WithMany("ProjectEmployees")
                        .HasForeignKey("OfferRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferUsedItem", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Offer", "Offer")
                        .WithMany("UsedItems")
                        .HasForeignKey("OfferRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Raunstrup.Service.Infrastructure.Entities.OfferWorkingHours", b =>
                {
                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Raunstrup.Service.Infrastructure.Entities.Offer", "Offer")
                        .WithMany("WorkingHours")
                        .HasForeignKey("OfferRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
