using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Account
{
    public class RegisterUserVM
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Password is not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
