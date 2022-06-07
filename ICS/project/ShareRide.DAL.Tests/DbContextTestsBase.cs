using System;
using System.Threading.Tasks;
using ShareRide.Common.Tests;
using ShareRide.Common.Tests.Factories;
using ShareRide.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using ShareRide.DAL;
using Xunit;
using Xunit.Abstractions;

namespace ShareRide.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        ShareRideDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<ShareRideDbContext> DbContextFactory { get; }
    protected ShareRideDbContext ShareRideDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await ShareRideDbContextSUT.Database.EnsureDeletedAsync();
        await ShareRideDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ShareRideDbContextSUT.Database.EnsureDeletedAsync();
        await ShareRideDbContextSUT.DisposeAsync();
    }
}