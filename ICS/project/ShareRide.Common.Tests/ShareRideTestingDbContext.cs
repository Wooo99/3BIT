using ShareRide.Common.Tests.Seeds;
using ShareRide.DAL;
using Microsoft.EntityFrameworkCore;

namespace ShareRide.Common.Tests
{
    public class ShareRideTestingDbContext : ShareRideDbContext
    {
        private readonly bool _seedTestingData;

        public ShareRideTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
            : base(contextOptions, seedDemoData: false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_seedTestingData)
            {
                UserSeeds.Seed(modelBuilder);
                //AddressSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                RideSeeds.Seed(modelBuilder);
            }
        }
    }
}