using ShareRide.DAL;
using Microsoft.EntityFrameworkCore;

namespace ShareRide.Common.Tests.Factories
{
    public class DbContextTestingInMemoryFactory : IDbContextFactory<ShareRideDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextTestingInMemoryFactory(string databaseName, bool seedTestingData = false)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public ShareRideDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<ShareRideDbContext> contextOptionsBuilder = new();
            contextOptionsBuilder.UseInMemoryDatabase(_databaseName);

            // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            // builder.EnableSensitiveDataLogging();

            return new ShareRideTestingDbContext(contextOptionsBuilder.Options, _seedTestingData);
        }
    }
}