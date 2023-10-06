using System.ComponentModel.DataAnnotations;

namespace ConnectionResiliency.Models
{
    public class BaseClass
    {
        [Key]
        public int BaseID { get; set; }
    }
}
