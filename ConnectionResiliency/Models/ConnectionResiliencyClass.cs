using ConnectionResiliency.DbContexts;
using ConnectionResiliency.DbTables;
using Microsoft.EntityFrameworkCore;

namespace ConnectionResiliency.Models
{
    public class ConnectionResiliencyClass : IConnectionResiliency
    {
        private readonly ExampleDbContext _exampleDbContext;
        public ConnectionResiliencyClass(ExampleDbContext exampleDbContext)
        {
            _exampleDbContext = exampleDbContext;
        }
        public async Task AddUsers(Users users)
        {
          var Strategy= _exampleDbContext.Database.CreateExecutionStrategy();

         await Strategy.ExecuteAsync(async() =>
            {
                 var transaction = await _exampleDbContext.Database.BeginTransactionAsync();
                 _exampleDbContext.Users.Add(users);
                 await Task.Delay(5000); //burayı kontrol et
                 _exampleDbContext.SaveChanges();
                 await  transaction.CommitAsync();
            });
        }
    }
}
