using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Models
{
    public class Dependent : CommonPersonProperties
    {
        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = default!;
    }
}
