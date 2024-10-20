namespace Comany_Managment_System_MVC_.ViewModels.Dependent
{
    public class CreateDependentVM : CommonDependentVM
    {
        public int id;
        public IFormFile Image { get; set; } = default!;
    }
}
