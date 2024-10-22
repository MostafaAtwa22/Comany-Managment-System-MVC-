using Comany_Managment_System_MVC_.Attributes;
using Comany_Managment_System_MVC_.Settings;

namespace Comany_Managment_System_MVC_.ViewModels.Dependent
{
    public class CreateDependentVM : CommonDependentVM
    {
        public int id;

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Image { get; set; } = default!;
    }
}
