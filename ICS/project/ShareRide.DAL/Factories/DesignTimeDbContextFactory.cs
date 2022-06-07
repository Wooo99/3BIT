using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShareRide.DAL.Factories
{
    /// <summary>
    /// EF Core CLI migration generation uses this DbContext to create model and migration
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShareRideDbContext>
    {
        public ShareRideDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ShareRideDbContext> builder = new();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = ShareRide;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");

            return new ShareRideDbContext(builder.Options);
        }
    }
}
