namespace Comany_Managment_System_MVC_.Services.ViewModels.Dependents
{
    public class CreateDependentVM : CommonDependentVM
    {
        public int id;

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Image { get; set; } = default!;
    }
}
