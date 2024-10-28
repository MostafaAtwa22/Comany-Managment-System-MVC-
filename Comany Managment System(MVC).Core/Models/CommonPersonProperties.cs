using Comany_Managment_System_MVC_.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class CommonPersonProperties : CommonProperties
    {
        [Required]
        public int Age { get; set; }

        public string Image { get; set; } = default!;

        [Required]
        public Gender Gender { get; set; }
    }
}
