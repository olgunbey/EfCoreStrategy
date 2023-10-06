using ConnectionResiliency.DbTables;

namespace ConnectionResiliency.Models
{
    public interface IException<T> where T : BaseClass,new()
    {
        public Task<string> Exceptions(Func<T,Task> func,T users);
    }
}
