﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LocationAPI.Data
{
    public class Context: DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationsToScooters> LinkLocationsToScooterInstances { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=trotis_db;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Guid> scooters = new List<Guid>();
            scooters.Add(Guid.Parse("65a1ecd5-5ac1-434b-8c35-1bedcee04dd4"));
            Location location = Location.Create(27.5743639M, 47.1739724M, "TrotIS Center, Faculty of Computer Science");
            
            modelBuilder.Entity<Location>()
                .HasData(location);

            modelBuilder.Entity<LocationsToScooters>()
                .HasKey(a => new { a.LocationID, a.ScooterInstanceID });

            modelBuilder.Entity<LocationsToScooters>()
                .HasData(LocationsToScooters.CreateMany(location.LocationID, scooters));
        }
    }
}
