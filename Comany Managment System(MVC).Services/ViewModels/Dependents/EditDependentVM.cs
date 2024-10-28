namespace Comany_Managment_System_MVC_.Services.ViewModels.Dependents
{
    public class EditDependentVM : CommonDependentVM
    {
        public string? CurrentImage { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; } = default!;
    }
}
