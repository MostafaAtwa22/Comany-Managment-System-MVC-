namespace Comany_Managment_System_MVC_.ViewModels.Employee
{
    public class EditEmployeeVM : CommonEmployeeVM
    {
        public int Id { get; set; }

        public string? CurrentImage { get; set; }

        public IFormFile? Image { get; set; } = default!;
    }
}
