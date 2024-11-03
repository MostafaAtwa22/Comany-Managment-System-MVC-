using Comany_Managment_System_MVC_.Core.Enums;
using Company_Management_System_MVC_.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class Dependent : CommonPersonProperties
    {
        [Required]
        public Relation Relation { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = default!;
    }
}
