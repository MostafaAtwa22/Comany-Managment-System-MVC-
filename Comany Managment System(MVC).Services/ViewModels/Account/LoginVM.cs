using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string Email{ get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } 
    }

}
