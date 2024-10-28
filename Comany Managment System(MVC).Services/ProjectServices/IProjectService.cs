
namespace Comany_Managment_System_MVC_.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<IEnumerable<SelectListItem>> GetSelectList();
    }
}
