﻿using Microsoft.AspNetCore.Mvc;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Projects
{
    public class CommonProjectVM
    {
        [Required]
        [MaxLength(50), MinLength(3)]
        [Remote(action: "IsNameUnique",
            controller: "Projects",
            AdditionalFields = "Id,DepartmentId",
            ErrorMessage = "This Project is already exists.")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1_000, 100_000_000)]
        public int Budget { get; set; }

        [Required]
        [MaxLength(50), MinLength(4)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
