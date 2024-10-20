namespace Comany_Managment_System_MVC_.ViewModels.Dependent
{
    public class EditDependentVM : CommonDependentVM
    {
        public string? CurrentImage { get; set; }

        public IFormFile? Image { get; set; } = default!;
    }
}
