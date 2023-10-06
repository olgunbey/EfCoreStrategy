using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConnectionResiliency.DbContexts
{
    public class DesignTime : IDesignTimeDbContextFactory<ExampleDbContext>
    {
        public ExampleDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ExampleDbContext> dbContextOptions = new();

            dbContextOptions.UseSqlServer("Server=127.0.0.1,4149;Database=ExampleDb;User Id=SA;Password=Password123;TrustServerCertificate=True");

            return new ExampleDbContext(dbContextOptions.Options);
        }
    }
}
