using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.ViewModels.Department
{
    public class CommonDepartmentVM
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = null!;

        [Required]
        [Display(Name = "Manager Name")]
        public int? ManagerId { get; set; }

        public IEnumerable<SelectListItem> ManagersSelectList = Enumerable.Empty<SelectListItem>();
    }
}
