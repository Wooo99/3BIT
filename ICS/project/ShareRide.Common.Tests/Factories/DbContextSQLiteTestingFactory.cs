using ShareRide.DAL;
using Microsoft.EntityFrameworkCore;

namespace ShareRide.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory : IDbContextFactory<ShareRideDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public ShareRideDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ShareRideDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new ShareRideTestingDbContext(builder.Options, _seedTestingData);
    }
}