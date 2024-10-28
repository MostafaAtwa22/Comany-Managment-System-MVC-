using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
