using Comany_Managment_System_MVC_.Attributes;
using Comany_Managment_System_MVC_.Settings;

namespace Comany_Managment_System_MVC_.ViewModels.Employee
{
    public class CreateEmployeeVM : CommonEmployeeVM
    {
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Image { get; set; } = default!;
    }
}
