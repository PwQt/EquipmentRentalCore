using EquipmentRentalCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Data
{
    public class EquipmentRentalContext : IdentityDbContext<User, ApplicationRole, int>
    {
        public EquipmentRentalContext(DbContextOptions<EquipmentRentalContext> options) : base(options)
        {

        }

        
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipment>().ToTable("Equipments");
            modelBuilder.Entity<EquipmentType>().ToTable("EquipmentTypes");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Rental>().ToTable("Rentals");
            modelBuilder.Entity<Room>().ToTable("Rooms");
        }
    }
}
