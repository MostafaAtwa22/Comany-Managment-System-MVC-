﻿using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Models
{
    public class EmployeeProjects
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = default!;

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; } = default!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Hours { get; set; }
    }
}
