using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using ShareRide.Common.Enums;
using ShareRide.DAL.Entities;

namespace ShareRide.DAL.Seeds;

public static class CarSeeds
{

    public static readonly CarEntity TestCar1 = new(
        Id: Guid.NewGuid(),
        Name: "test name",
        Manufacturer: Manufacturer.Peugeot,
        CarType: CarType.Sedan,
        RegistrationYear: 2000,
        PhotoPath: null,
        PassengerSeats: 4,
        OwnerGuid: UserSeeds.TestUser1.Id)
    {
        Owner = UserSeeds.TestUser1,
    };


    public static readonly CarEntity TestCar2 = new(
        Id: Guid.NewGuid(),
        Name: "my_first_car",
        Manufacturer: Manufacturer.BMW,
        CarType: CarType.Combi,
        RegistrationYear: 2008,
        PhotoPath: null,
        PassengerSeats: 5,
        OwnerGuid: UserSeeds.TestUser2.Id)
    {
        Owner = UserSeeds.TestUser1,
    };


    public static readonly CarEntity TestCar3 = new(
        Id: Guid.NewGuid(),
        Name: "my_second_car",
        Manufacturer: Manufacturer.Skoda,
        CarType: CarType.Combi,
        RegistrationYear: 2004,
        PhotoPath: null,
        PassengerSeats: 5,
        OwnerGuid: UserSeeds.TestUser3.Id)
    {
        Owner = UserSeeds.TestUser1,
    };

    public static readonly CarEntity TestCar4 = new(
        Id: Guid.NewGuid(),
        Name: "my_third_car",
        Manufacturer: Manufacturer.Volkswagen,
        CarType: CarType.Sedan,
        RegistrationYear: 2020,
        PhotoPath: null,
        PassengerSeats: 4,
        OwnerGuid: UserSeeds.TestUser4.Id)
    {
        Owner = UserSeeds.TestUser1,
    };

    public static readonly CarEntity TestCar5 = new(
        Id: Guid.NewGuid(),
        Name: "my_last_car",
        Manufacturer: Manufacturer.Honda,
        CarType: CarType.Motorcycle,
        RegistrationYear: 1975,
        PhotoPath: null,
        PassengerSeats: 2,
        OwnerGuid: UserSeeds.TestUser5.Id)
    {
        Owner = UserSeeds.TestUser1,
    };


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(TestCar1 with { Owner = null});
        modelBuilder.Entity<CarEntity>().HasData(TestCar2 with { Owner = null});
        modelBuilder.Entity<CarEntity>().HasData(TestCar3 with { Owner = null });
        modelBuilder.Entity<CarEntity>().HasData(TestCar4 with { Owner = null });
        modelBuilder.Entity<CarEntity>().HasData(TestCar5 with { Owner = null });
    }
}