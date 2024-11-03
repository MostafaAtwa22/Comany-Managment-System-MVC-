namespace Comany_Managment_System_MVC_.Services.ViewModels.Dependents
{
    public class EditDependentVM : CommonDependentVM
    {
        public int Id { get; set; }

        public string? CurrentImage { get; set; }

        public Employee Employee { get; set; } = new Employee();


        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; } = default!;
    }
}
