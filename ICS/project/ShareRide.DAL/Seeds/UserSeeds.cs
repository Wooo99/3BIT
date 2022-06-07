using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using ShareRide.DAL.Entities;

namespace ShareRide.DAL.Seeds;

public static class UserSeeds
{

    public static readonly UserEntity TestUser1 = new(
        Id: Guid.NewGuid(),
        FirstName: "Jakub",
        LastName: "Kovacik",
        PhotoPath: null)
    {
        //OwnedCars = new List<CarEntity>()
    };

    public static readonly UserEntity TestUser2 = new(
        Id: Guid.NewGuid(),
        FirstName: "Denis",
        LastName: "Fermdasius",
        PhotoPath: null)
    {
        //OwnedCars = new List<CarEntity>()
    };

    public static readonly UserEntity TestUser3 = new(
        Id: Guid.NewGuid(),
        FirstName: "Adam",
        LastName: "Policka",
        PhotoPath: null)
    {
        //OwnedCars = new List<CarEntity>()
    };

    public static readonly UserEntity TestUser4 = new(
        Id: Guid.NewGuid(),
        FirstName: "Pavol",
        LastName: "Osinka",
        PhotoPath: null)
    {
        //OwnedCars = new List<CarEntity>()
    };

    public static readonly UserEntity TestUser5 = new(
        Id: Guid.NewGuid(),
        FirstName: "Michal",
        LastName: "Reznik",
        PhotoPath: null)
    {
        //OwnedCars = new List<CarEntity>()
    };

    static UserSeeds()
    {
        //TestUser1.OwnedCars.Add(CarSeeds.TestCar1);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(TestUser1);
        modelBuilder.Entity<UserEntity>().HasData(TestUser2);
        modelBuilder.Entity<UserEntity>().HasData(TestUser3);
        modelBuilder.Entity<UserEntity>().HasData(TestUser4);
        modelBuilder.Entity<UserEntity>().HasData(TestUser5);

    }
}