
namespace Comany_Managment_System_MVC_.Services.ViewModels.Dependents
{
    public class CommonDependentVM
    {
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Gender")]
        public int Gender { get; set; }

        public IEnumerable<SelectListItem> GenderSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required]
        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }

        public IEnumerable<SelectListItem> EmployeesList = Enumerable.Empty<SelectListItem>();
    }
}
