using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Entities;

namespace Raunstrup.Service.Infrastructure.Database
{
    public class RaunstrupContext : DbContext
    {
        public RaunstrupContext(DbContextOptions<RaunstrupContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferWorkingHours> WorkingHours { get; set; }
        public DbSet<OfferDriving> OfferDrivings { get; set; }
        public DbSet<OfferEmployee> OfferEmployees { get; set; }
        public DbSet<OfferAssignedItem> OfferAssignedItems { get; set; }
        public DbSet<OfferUsedItem> OfferUsedItems { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Employee>().ToTable("Employee");

            modelBuilder.Entity<Offer>().ToTable("Offer");
            modelBuilder.Entity<OfferWorkingHours>().ToTable("OfferWorkingHours");
            modelBuilder.Entity<OfferDriving>().ToTable("OfferDriving");
            modelBuilder.Entity<OfferEmployee>().ToTable("OfferEmployee");
            modelBuilder.Entity<OfferAssignedItem>().ToTable("OfferAssignedItem");
            modelBuilder.Entity<OfferUsedItem>().ToTable("OfferUsedItem");

            modelBuilder.Entity<Profession>().ToTable("Profession");
            modelBuilder.Entity<Campaign>().ToTable("Campaign");


        }
    }
}
