using System;
using System.Linq;
using System.Threading.Tasks;
using ShareRide.DAL.Entities;
using ShareRide.DAL.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShareRide.Common;
using ShareRide.Common.Enums;
using ShareRide.Common.Tests;
using ShareRide.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace ShareRide.DAL.Tests
{
    public class DbContextUserTests : DbContextTestsBase
    {
        //private readonly ShareRideDbContext _shareRideDbContextSut;
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetAll_Users_SeededUserExists()
        {
            var userEntityFromDb = await ShareRideDbContextSUT.Users.SingleOrDefaultAsync(u => u.Id == UserSeeds.TestUser.Id);         
            
            Assert.Equal(UserSeeds.TestUser.Id, userEntityFromDb.Id);
        }

        [Fact]
        public async Task Delete_SeededUser()
        {
            ShareRideDbContextSUT.Users.Remove(UserSeeds.TestUser);
            await ShareRideDbContextSUT.SaveChangesAsync();

            Assert.Null(await ShareRideDbContextSUT.Users.SingleAsync(u => u.Id == UserSeeds.TestUser.Id));
        }

        [Fact]
        public async Task Delete_New_User()
        {
            //Arrange
            var user = new UserEntity(
                Id: Guid.NewGuid(),
                FirstName: "Mike",
                LastName: "Testowski",
                PhotoPath: String.Empty)
            { 
                OwnedCars = new List<CarEntity>()
            };

            var car = new CarEntity(
                Id: Guid.NewGuid(),
                Name: "Test",
                Manufacturer: Manufacturer.Subaru,
                CarType: CarType.Sedan,
                RegistrationYear: 2004,
                PhotoPath: String.Empty,
                PassengerSeats: 3,
                OwnerGuid: user.Id);

            ShareRideDbContextSUT.Users.Add(user);
            await ShareRideDbContextSUT.SaveChangesAsync();
            //Act
            ShareRideDbContextSUT.Users.Remove(user);
            await ShareRideDbContextSUT.SaveChangesAsync();
            //Assert
            Assert.False(await ShareRideDbContextSUT.Users.AnyAsync(i => i.Id == user.Id));
            Assert.False(await ShareRideDbContextSUT.Cars.AnyAsync(i => i.Id == car.Id));
        }
    }
}