using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CityInfo.API.DbContexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Tehran" },
                new City { Id = 2, Name = "Shiraz" });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest { Id = 1, Name = "Hafez", Description = "Hafez", CityId = 2 },
                new PointOfInterest { Id = 2, Name = "Khajo", Description = "Khajo", CityId = 2 },
                new PointOfInterest { Id = 3, Name = "Milad Tower", Description = "Milad Tower", CityId = 1 });
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }
    }
}
