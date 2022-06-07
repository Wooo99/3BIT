using System;
using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using ShareRide.DAL.Entities;
using ShareRide.Common.Tests.Seeds;

namespace ShareRide.Common.Tests.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity EmptyRide = new(
        Id: default,
        //StartGuid: default,
        //DestinationGuid: default,
        Start: String.Empty,
        Destination: String.Empty,
        StartTime: default,
        EstimatedEndTime: default,
        DriverGuid: default,
        CarId: default
        );

    public static readonly RideEntity RideTest = new(
        Id: Guid.Parse(input: "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        //StartGuid: AddressSeeds.TestAddress.Id,
        //DestinationGuid: AddressSeeds.TestAddress.Id,
        Start: "Slavkov",
        Destination: "Brno",
        StartTime: new DateTime(2022, 04, 10, 08, 00, 00),
        EstimatedEndTime: new DateTime(2022, 04, 10, 10, 00, 00),
        DriverGuid: UserSeeds.TestUser.Id,
        CarId: CarSeeds.TestCar.Id)
    {
        //Start = AddressSeeds.TestAddress,
        //Destination = AddressSeeds.TestAddress,
        Driver = UserSeeds.TestUser,
        Car = CarSeeds.TestCar
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(
            RideTest with
            {
                Passengers = Array.Empty<UserEntity>(),
                Start = "Slavkov",
                Destination = "Brno",
                Driver = null,
                Car = null
            }
        );
    }
}