using ConnectionResiliency.DbTables;
using Microsoft.EntityFrameworkCore;

namespace ConnectionResiliency.DbContexts
{
    public class ExampleDbContext:DbContext
    {
        public ExampleDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(x => x.BaseID);
        }
    }
}
