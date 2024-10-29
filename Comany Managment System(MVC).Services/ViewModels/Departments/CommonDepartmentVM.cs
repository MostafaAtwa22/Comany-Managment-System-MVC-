
using Microsoft.AspNetCore.Mvc;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Departments
{
    public class CommonDepartmentVM
    {
        [Required]
        [MaxLength(50)]
        [Remote(action: "IsNameUnique",
            controller: "Departments",
            AdditionalFields = "Id",
            ErrorMessage = "This Department is already exists.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Location { get; set; } = null!;


        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Manager Name")]
        public int? ManagerId { get; set; }

        public IEnumerable<SelectListItem> ManagersSelectList = Enumerable.Empty<SelectListItem>();
    }
}
