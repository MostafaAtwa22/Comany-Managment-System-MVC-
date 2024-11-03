using Comany_Managment_System_MVC_.Core.Enums;
using Company_Management_System_MVC_.Core.Enums;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Dependents
{
    public class CommonDependentVM
    {
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public Relation Relation { get; set; }
        public IEnumerable<SelectListItem> RelaionSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required]
        [Display(Name = "Gender")]
        public int Gender { get; set; }

        public IEnumerable<SelectListItem> GenderSelectList { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required]
        public int EmployeeId { get; set; }
    }
}
