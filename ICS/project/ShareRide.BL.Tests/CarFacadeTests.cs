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
    public sealed class CarFacadeTests : CRUDFacadeTestsBase
    {
        private readonly CarFacade _carFacadeSUT;

        public CarFacadeTests(ITestOutputHelper output) : base(output)
        {
            _carFacadeSUT = new CarFacade(UnitOfWorkFactory, Mapper);
        }
        
        [Fact]
        public async Task Get_By_Car_Id()
        {
            var car = await _carFacadeSUT.GetAsync(CarSeeds.TestCar.Id);

            DeepAssert.Equal(Mapper.Map<CarDetailModel>(CarSeeds.TestCar), car);
        }

        [Fact]
        public async Task Get_By_User_Id()
        {
            var cars = await _carFacadeSUT.GetByOwnerAsync(UserSeeds.TestUser.Id);
            var car = cars.SingleOrDefault(i => i.Id == CarSeeds.TestCar.Id);

            DeepAssert.Equal(Mapper.Map<CarListModel>(CarSeeds.TestCar), car);
        }

        [Fact]
        public async Task GetAll_Single_SeededCar()
        {
            var cars = await _carFacadeSUT.GetAsync();
            var car = cars.Single(i => i.Id == CarSeeds.TestCar.Id);

            DeepAssert.Equal(Mapper.Map<CarListModel>(CarSeeds.TestCar),  car);
        }

        [Fact]
        public async Task Save_New_DoesNotThrow()
        {
            //Arrange
            CarDetailModel model = new CarDetailModel(
                Manufacturer: Manufacturer.Donkervoort,
                CarType: CarType.Sedan,
                RegistrationYear: 2022,
                Name: "TestVehicle2",
                PassengerSeats: 1,
                OwnerGuid: UserSeeds.TestUser.Id
                );
            //Act
                var _ = await _carFacadeSUT.SaveAsync(model);
        }

        [Fact]
        public async Task Save_New_Persisted()
        {
            //Arrange
            CarDetailModel model = new CarDetailModel(
                Manufacturer: Manufacturer.Donkervoort,
                CarType: CarType.Sedan,
                RegistrationYear: 2022,
                Name: "TestVehicle2",
                PassengerSeats: 1,
                OwnerGuid: UserSeeds.TestUser.Id
                );
            //Act
            var _ = await _carFacadeSUT.SaveAsync(model);
            //Assert
            var cars = await _carFacadeSUT.GetByOwnerAsync(UserSeeds.TestUser.Id);
            var car = cars.Single(i => i.Name == "TestVehicle2");
            Assert.NotEqual(car.Id, Guid.Empty);
        }

        [Fact]
        public async Task Update_Model()
        {
            //Arrange
            var model = new CarDetailModel(
                Manufacturer: CarSeeds.TestCar.Manufacturer,
                CarType: CarSeeds.TestCar.CarType,
                RegistrationYear: CarSeeds.TestCar.RegistrationYear,
                Name: CarSeeds.TestCar.Name,
                PassengerSeats: CarSeeds.TestCar.PassengerSeats,
                OwnerGuid: UserSeeds.TestUser.Id)
            { 
                Id = CarSeeds.TestCar.Id
            };
            //Act
            model.Name = "TestVehicle3";
            await _carFacadeSUT.SaveAsync(model);
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var car = await dbx.Cars.SingleAsync(i => i.Id == CarSeeds.TestCar.Id);
            DeepAssert.Equal(model, Mapper.Map<CarDetailModel>(car));
        }

        [Fact]
        public async Task Delete_Seeded_Car()
        {
            await _carFacadeSUT.DeleteAsync(CarSeeds.TestCar.Id);
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbx.Cars.AnyAsync(i => i.Id == CarSeeds.TestCar.Id));
        }
    }
}
