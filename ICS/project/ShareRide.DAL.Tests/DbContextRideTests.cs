using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using ShareRide.Common.Enums;
using ShareRide.Common.Tests;
using ShareRide.Common.Tests.Seeds;
using ShareRide.DAL.Entities;
using System.Collections.Generic;

namespace ShareRide.DAL.Tests
{
    /// <summary>
    /// Deep assert does not like RideEntity so we use regular assert and check properties manually
    /// </summary>
    public class DbContextRideTests : DbContextTestsBase
    {
        public DbContextRideTests(ITestOutputHelper output) : base(output)
        {
        }
        
        [Fact]
        public async Task AddNew_No_Passengers()
        {
            //Arrange
            var driver = UserSeeds.TestUser;
            var car = CarSeeds.TestCar;
            var ride = RideSeeds.EmptyRide with
            {
                Id = Guid.NewGuid(),
                Start = "Bratislava",
                Destination = "Brno",
                //StartGuid = AddressSeeds.TestAddress.Id,
                //DestinationGuid = AddressSeeds.TestAddress.Id,
                StartTime = new DateTime(2022, 5, 1),
                EstimatedEndTime = new DateTime(2022, 5, 1),
                DriverGuid = UserSeeds.TestUser.Id,
                CarId = CarSeeds.TestCar.Id,
            };

            //Act
            ShareRideDbContextSUT.Rides.Add(ride);
            await ShareRideDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualRide = await dbx.Rides.SingleOrDefaultAsync(entity => entity.Id == ride.Id);
            DeepAssert.Equal(ride, actualRide);

        }

        [Fact]
        public async Task GetAll_Rides_ContainsSeededRideTest()
        {

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualRide = await dbx.Rides.SingleOrDefaultAsync(i => i.Id == RideSeeds.RideTest.Id);
            Assert.Equal(RideSeeds.RideTest.Id, actualRide.Id);
        }

        [Fact]
        public async Task AddNew_Contains_Passenger()
        {
            //Arrange
            var passenger = new UserEntity
            (
                Id: Guid.NewGuid(),
                FirstName: "Test",
                LastName: "Testowski",
                PhotoPath: String.Empty
            )
            {
            };

            var ride = new RideEntity(
                Id: Guid.NewGuid(),
                Start: "Bratislava",
                Destination: "Brno",
                //StartGuid: AddressSeeds.TestAddress.Id,
                //DestinationGuid: AddressSeeds.TestAddress.Id,
                StartTime: new DateTime(2022, 5, 5),
                EstimatedEndTime: new DateTime(2022, 6, 6),
                DriverGuid: UserSeeds.TestUser.Id,
                CarId: CarSeeds.TestCar.Id)
            {
                Passengers = new List<UserEntity> { passenger }
            };

            //Act
            ShareRideDbContextSUT.Rides.Add(ride);
            await ShareRideDbContextSUT.SaveChangesAsync();
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualRide = await dbx.Rides
                .Include(i => i.Passengers)
                .SingleOrDefaultAsync(entity => entity.Id == ride.Id);
            DeepAssert.Equal(ride, actualRide);
        }

        [Fact]
        public async Task Delete_Seeded_Ride()
        {
            Assert.True(await ShareRideDbContextSUT.Rides.AnyAsync(i => i.Id == RideSeeds.RideTest.Id));
            //Act
            ShareRideDbContextSUT.Rides.Remove(RideSeeds.RideTest);
            await ShareRideDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.False(await ShareRideDbContextSUT.Rides.AnyAsync(i => i.Id == RideSeeds.RideTest.Id));
        }
    }
}