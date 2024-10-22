using Comany_Managment_System_MVC_.Attributes;
using Comany_Managment_System_MVC_.Settings;

namespace Comany_Managment_System_MVC_.ViewModels.Dependent
{
    public class EditDependentVM : CommonDependentVM
    {
        public string? CurrentImage { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; } = default!;
    }
}
