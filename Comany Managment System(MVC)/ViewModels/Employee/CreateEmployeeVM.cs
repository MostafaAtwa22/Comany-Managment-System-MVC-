namespace Comany_Managment_System_MVC_.ViewModels.Employee
{
    public class CreateEmployeeVM : CommonEmployeeVM
    {
        public IFormFile Image { get; set; } = default!;
    }
}
