using System;
using System.Linq;
using System.Threading.Tasks;
using ShareRide.DAL.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShareRide.Common;
using ShareRide.Common.Enums;
using ShareRide.Common.Tests;
using ShareRide.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace ShareRide.DAL.Tests
{
    public class DbContextCarTests : DbContextTestsBase
    {
        public DbContextCarTests(ITestOutputHelper output) : base(output)
        {
        }
        
        [Fact]
        public async Task AddNewCar()
        {
            //Arrange
            var user = UserSeeds.TestUser;
            var car = CarSeeds.EmptyCar with
            {
                Id = Guid.NewGuid(),
                Name = "test car",
                Manufacturer = Manufacturer.Abarth,
                CarType = CarType.Combi,
                RegistrationYear = 2000,
                PhotoPath = null,
                PassengerSeats = 1,
                OwnerGuid = user.Id
            };

            //Act
            //ShareRideDbContextSUT.Users.Add(user);
            ShareRideDbContextSUT.Cars.Add(car);
            await ShareRideDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Cars
                .SingleAsync(i => i.Id == car.Id);
            DeepAssert.Equal(car, actualEntity);
        }

        [Fact]
        public async Task Delete_SeededCar()
        {
            ShareRideDbContextSUT.Cars.Remove(CarSeeds.TestCar);
            await ShareRideDbContextSUT.SaveChangesAsync();

            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            Assert.Null(dbx.Cars.Single(i => i.Id == CarSeeds.TestCar.Id));
        }

        [Fact]
        public async Task Delete_Car_No_Rides()
        {
            //Arrange
            var user = UserSeeds.TestUser;
            var car = CarSeeds.EmptyCar with
            {
                Id = Guid.NewGuid(),
                Name = "test car",
                Manufacturer = Manufacturer.Abarth,
                CarType = CarType.Combi,
                RegistrationYear = 2000,
                PhotoPath = null,
                PassengerSeats = 1,
                OwnerGuid = user.Id
            };

            //Act
            //ShareRideDbContextSUT.Users.Add(user);
            ShareRideDbContextSUT.Cars.Add(car);
            await ShareRideDbContextSUT.SaveChangesAsync();
            ShareRideDbContextSUT.Cars.Remove(car);
            await ShareRideDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.False(await ShareRideDbContextSUT.Cars.AnyAsync(i => i.Id == car.Id));
        }
    }
}