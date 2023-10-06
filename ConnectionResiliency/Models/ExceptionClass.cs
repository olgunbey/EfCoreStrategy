using ConnectionResiliency.DbTables;

namespace ConnectionResiliency.Models
{
    public class ExceptionClass<T> : IException<T> where T : BaseClass, new()
    {
        public async Task<string> Exceptions(Func<T,Task> func, T u)
        {
			try
			{
				await func.Invoke(u);
				return Outputs.Success;
			}
			catch (Microsoft.EntityFrameworkCore.Storage.RetryLimitExceededException e)
			{
				return Outputs.FailtureRetryLimited;
			}
			catch (Exception e)
			{
				return Outputs.Failture;
			}
        }
    }
}
