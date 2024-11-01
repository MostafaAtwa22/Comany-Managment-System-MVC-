using Microsoft.AspNetCore.Identity;

namespace Comany_Managment_System_MVC_.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int EmployeeId { get; set; }
    }
}
