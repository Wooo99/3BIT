using System;
using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using ShareRide.Common.Enums;
using ShareRide.Common.Tests.Seeds;
using ShareRide.DAL.Entities;

namespace ShareRide.Common.Tests.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity EmptyCar = new(
        Id: default,
        Name: default,
        Manufacturer: default,
        CarType: default,
        RegistrationYear: default,
        PhotoPath: default,
        PassengerSeats: default,
        OwnerGuid: default)
    {
        Owner = default
    };

    public static readonly CarEntity TestCar = new(
        Id: Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        Name: "test name",
        Manufacturer: Manufacturer.Peugeot,
        CarType: CarType.Sedan,
        RegistrationYear: 2000,
        PhotoPath: null,
        PassengerSeats: 4,
        OwnerGuid: UserSeeds.TestUser.Id)
    {
        Owner = UserSeeds.TestUser,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(TestCar with { Owner = null});
    }
}