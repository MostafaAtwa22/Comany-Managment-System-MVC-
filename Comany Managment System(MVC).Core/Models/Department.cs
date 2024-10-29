using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class Department : CommonProperties
    {
        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public int? ManagerId { get; set; }

        public virtual Employee? Manager { get; set; } = default!;

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
