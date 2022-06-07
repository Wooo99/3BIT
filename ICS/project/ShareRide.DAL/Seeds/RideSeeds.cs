using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using ShareRide.DAL.Entities;

namespace ShareRide.DAL.Seeds;

public static class RideSeeds
{
    public static readonly RideEntity TestRide1 = new(
        Id: Guid.NewGuid(),
        Start: "Brno",
        Destination: "Slavkov",
        StartTime: new DateTime(2022, 04, 10, 08, 00, 00),
        EstimatedEndTime: new DateTime(2022, 04, 10, 10, 00, 00),
        DriverGuid: UserSeeds.TestUser1.Id,
        CarId: CarSeeds.TestCar1.Id)
    {

    };

    public static readonly RideEntity TestRide2 = new(
        Id: Guid.NewGuid(),
        Start: "Olomouc",
        Destination: "Praha",
        StartTime: new DateTime(2022, 05, 10, 10, 00, 00),
        EstimatedEndTime: new DateTime(2022, 05, 10, 12, 00, 00),
        DriverGuid: UserSeeds.TestUser2.Id,
        CarId: CarSeeds.TestCar2.Id)
    {

    };

    public static readonly RideEntity TestRide3 = new(
        Id: Guid.NewGuid(),
        Start: "Ostrava",
        Destination: "Pardubice",
        StartTime: new DateTime(2022, 06, 10, 08, 00, 00),
        EstimatedEndTime: new DateTime(2022, 06, 10, 10, 00, 00),
        DriverGuid: UserSeeds.TestUser3.Id,
        CarId: CarSeeds.TestCar3.Id)
    {

    };

    public static readonly RideEntity TestRide4 = new(
        Id: Guid.NewGuid(),
        Start: "Uhersky Brod",
        Destination: "Fridek-Mistek",
        StartTime: new DateTime(2022, 04, 11, 09, 00, 00),
        EstimatedEndTime: new DateTime(2022, 04, 11, 11, 00, 00),
        DriverGuid: UserSeeds.TestUser4.Id,
        CarId: CarSeeds.TestCar4.Id)
    {

    };

    public static readonly RideEntity TestRide5 = new(
        Id: Guid.NewGuid(),
        Start: "Brno",
        Destination: "Slavkov",
        StartTime: new DateTime(2022, 05, 15, 18, 00, 00),
        EstimatedEndTime: new DateTime(2022, 05, 15, 20, 00, 00),
        DriverGuid: UserSeeds.TestUser5.Id,
        CarId: CarSeeds.TestCar5.Id)
    {

    };

    public static readonly RideEntity TestRide6 = new(
        Id: Guid.NewGuid(),
        Start: "Brno",
        Destination: "Olomouc",
        StartTime: new DateTime(2022, 07, 15, 18, 00, 00),
        EstimatedEndTime: new DateTime(2022, 07, 15, 20, 00, 00),
        DriverGuid: UserSeeds.TestUser2.Id,
        CarId: CarSeeds.TestCar2.Id)
    {

    };

    public static readonly RideEntity TestRide7 = new(
        Id: Guid.NewGuid(),
        Start: "Hodonin",
        Destination: "Vltava",
        StartTime: new DateTime(2022, 06, 20, 18, 00, 00),
        EstimatedEndTime: new DateTime(2022, 06, 20, 20, 00, 00),
        DriverGuid: UserSeeds.TestUser4.Id,
        CarId: CarSeeds.TestCar4.Id)
    {

    };

    public static readonly RideEntity TestRide8 = new(
        Id: Guid.NewGuid(),
        Start: "Brno",
        Destination: "Slavkov",
        StartTime: new DateTime(2022, 06, 16, 19, 00, 00),
        EstimatedEndTime: new DateTime(2022, 06, 16, 22, 00, 00),
        DriverGuid: UserSeeds.TestUser1.Id,
        CarId: CarSeeds.TestCar1.Id)
    {

    };
    static RideSeeds()
    {
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RideEntity>().HasData(TestRide1 with 
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });

        modelBuilder.Entity<RideEntity>().HasData(TestRide2 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
        modelBuilder.Entity<RideEntity>().HasData(TestRide3 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
        modelBuilder.Entity<RideEntity>().HasData(TestRide4 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
        modelBuilder.Entity<RideEntity>().HasData(TestRide5 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
        modelBuilder.Entity<RideEntity>().HasData(TestRide6 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
        modelBuilder.Entity<RideEntity>().HasData(TestRide7 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
        modelBuilder.Entity<RideEntity>().HasData(TestRide8 with
        {
            Driver = null,
            Car = null,
            Passengers = null,
        });
    }
}