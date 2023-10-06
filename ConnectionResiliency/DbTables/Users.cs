using ConnectionResiliency.Models;
using System.ComponentModel.DataAnnotations;

namespace ConnectionResiliency.DbTables
{
    public class Users:BaseClass
    {
        public string UsersName { get; set; }
    }
}
