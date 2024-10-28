using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class CommonProperties : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
