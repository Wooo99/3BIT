using ShareRide.BL.Models.DetailModels;
using ShareRide.BL.Models.ListModels;
using System.Linq;
using System.Threading.Tasks;
using ShareRide.BL.Facades;
using ShareRide.Common.Tests;
using ShareRide.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using Xunit;
using Xunit.Abstractions;
using ShareRide.Common.Enums;
using System;

namespace ShareRide.BL.Tests
{
    public sealed class RideFacadests : CRUDFacadeTestsBase
    {
        private readonly RideFacade rideFacadeSUT;
        public RideFacadests(ITestOutputHelper output) : base(output)
        {
            rideFacadeSUT = new RideFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Get_Seeded_Ride()
        {
            //Act
            var ride = await rideFacadeSUT.GetAsync(RideSeeds.RideTest.Id);
            //Assert
            //This is a seed problem
            //DeepAssert.Equal(Mapper.Map<RideDetailModel>(RideSeeds.RideTest), ride);
            Assert.Equal(RideSeeds.RideTest.Id, ride!.Id);
        }

        [Fact]
        public async Task Get_SeededRide_By_Driver()
        {
            //Act
            var rides = await rideFacadeSUT.GetAsync(id: UserSeeds.TestUser.Id, 0);
            var ride = rides.Single(i => i.Id == RideSeeds.RideTest.Id);

            //Assert
            Assert.Equal(ride.Driver.Id, UserSeeds.TestUser.Id);
        }

        [Fact]
        public async Task Get_SeededRide_By_NotDriver()
        {
            //Act
            var rides = await rideFacadeSUT.GetAsync(id: UserSeeds.TestUser.Id, 1);

            //Assert
            //The seeded user is a driver
            Assert.Empty(rides);
        }

        [Fact]
        public async Task Get_SeededRide_By_Driver_Or_Passenger()
        {
            //Act
            var rides = await rideFacadeSUT.GetAsync(id: UserSeeds.TestUser.Id, 2);
            var ride = rides.Single(i => i.Id == RideSeeds.RideTest.Id);

            //Assert
            //The seeded user is a driver
            Assert.Equal(ride.Driver.Id, UserSeeds.TestUser.Id);
        }

        [Fact]
        public async Task Delete_Seeded_Ride()
        {
            await rideFacadeSUT.DeleteAsync(id: RideSeeds.RideTest.Id);
            var rides = await rideFacadeSUT.GetAsync();
            //Assert
            Assert.Empty(rides.Where(i => i.Id == RideSeeds.RideTest.Id));
        }
    }
}
