using Microsoft.EntityFrameworkCore;

namespace ShareRide.DAL.Factories;

public class SqlServerDbContextFactory : IDbContextFactory<ShareRideDbContext>
{
    private readonly string _connectionString;
    private readonly bool _seedDemoData;

    public SqlServerDbContextFactory(string connectionString, bool seedDemoData = true)
    {
        _connectionString = connectionString;
        _seedDemoData = seedDemoData;
    }

    public ShareRideDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShareRideDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);

        optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        optionsBuilder.EnableSensitiveDataLogging();

        return new ShareRideDbContext(optionsBuilder.Options, _seedDemoData);
    }
}