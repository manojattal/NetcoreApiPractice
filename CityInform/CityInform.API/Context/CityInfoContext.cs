using CityInform.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInform.API.Context
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(new City() { Id = 1, Name = "NYC", Description = "NYC Desc" },
                new City() { Id = 2, Name = "Pune", Description = "Pune Desc" });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest() { Id = 1, CityId = 1, Name = "Central Park" },
                    new PointOfInterest() { Id = 2, CityId = 2, Name = "Central Library" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
