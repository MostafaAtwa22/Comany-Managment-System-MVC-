namespace Comany_Managment_System_MVC_.Services.ViewModels.Employees
{
    public class CreateEmployeeVM : CommonEmployeeVM
    {
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Image { get; set; } = default!;
    }
}
