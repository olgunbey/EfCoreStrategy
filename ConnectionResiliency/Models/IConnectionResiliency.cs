using ConnectionResiliency.DbTables;

namespace ConnectionResiliency.Models
{
    public interface IConnectionResiliency
    {
        public Task AddUsers(Users users);
    }
}
