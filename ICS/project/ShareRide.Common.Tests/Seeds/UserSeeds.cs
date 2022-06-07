using System;
using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using ShareRide.DAL.Entities;

namespace ShareRide.Common.Tests.Seeds;

public static class UserSeeds
{
    
    public static readonly UserEntity EmptyUser = new(
        Id: default,
        FirstName: default,
        LastName: default,
        PhotoPath: default);
    
    public static readonly UserEntity TestUser = new(
        Id: Guid.Parse(input: "df935095-8709-4040-a2bb-b6f97cb416dc"),
        FirstName: "Jakub",
        LastName: "Kovacik",
        PhotoPath: null);

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(TestUser);
    }
}