﻿using EquipmentRentalCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Data
{
    public class EquipmentRentalContext : DbContext
    {
        public EquipmentRentalContext(DbContextOptions<EquipmentRentalContext> options) : base(options)
        {

        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>().ToTable("Equipments");
            modelBuilder.Entity<EquipmentType>().ToTable("EquipmentTypes");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Rental>().ToTable("Rentals");
        }
    }
}
