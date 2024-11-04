using Comany_Managment_System_MVC_.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class Employee : CommonPersonProperties, ISoftDeleteable
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = default!;

        public virtual Department DepartmentManager { get; set; } = default!;

        public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

        public virtual ICollection<EmployeeProjects>? EmployeeProjects { get; set; } = new List<EmployeeProjects>();

        public bool IsDeleted { get; set; }

        public DateTime? DateOFDelete { get; set; }
    }
}
