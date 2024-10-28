using System.ComponentModel.DataAnnotations;

namespace Comany_Managment_System_MVC_.Services.ViewModels.Account
{
    public class ForgetPasswordVM
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The email is not found !!!")]
        public string Email { get; set; } = string.Empty;
    }
}
