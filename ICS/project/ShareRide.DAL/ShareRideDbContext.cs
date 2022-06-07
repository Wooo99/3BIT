using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareRide.DAL.Entities;
using ShareRide.DAL.Seeds;

namespace ShareRide.DAL
{
    public class ShareRideDbContext : DbContext
    {
        private readonly bool _seedDemoData;


        public ShareRideDbContext(DbContextOptions contextOptions, bool seedDemoData = true) 
            :base(contextOptions)
        {
            _seedDemoData = seedDemoData;
        }
        public DbSet<RideEntity> Rides => Set<RideEntity>();
        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        //public DbSet<AddressEntity> Addresses => Set<AddressEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
          modelBuilder.Entity<UserEntity>()
                .HasMany(i => i.OwnedCars)
                .WithOne(i => i.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RideEntity>()
                .HasOne(i => i.Driver)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<RideEntity>()
                .HasOne(i => i.Car)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<RideEntity>()
                .HasMany<UserEntity>(i => i.Passengers)
                .WithMany(i => i.PassengerRides);
            
            modelBuilder.Entity<CarEntity>()
                .HasOne<UserEntity>(i => i.Owner)
                .WithMany(i => i.OwnedCars)
                .OnDelete(DeleteBehavior.Cascade);


            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                //AddressSeeds.Seed(modelBuilder);
                RideSeeds.Seed(modelBuilder);
            }
        }
    }
}
