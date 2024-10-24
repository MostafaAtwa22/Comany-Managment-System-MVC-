using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } 
    }

}
