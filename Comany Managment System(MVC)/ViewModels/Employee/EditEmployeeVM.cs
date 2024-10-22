using Comany_Managment_System_MVC_.Attributes;
using Comany_Managment_System_MVC_.Settings;

namespace Comany_Managment_System_MVC_.ViewModels.Employee
{
    public class EditEmployeeVM : CommonEmployeeVM
    {
        public int Id { get; set; }

        public string? CurrentImage { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; } = default!;
    }
}
