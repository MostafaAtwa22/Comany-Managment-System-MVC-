using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
