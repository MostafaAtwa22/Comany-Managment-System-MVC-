using Comany_Managment_System_MVC_.Services.ViewModels.Projects;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project?> Find(Expression<Func<Project, bool>> criteria);
        Task<IEnumerable<SelectListItem>> GetSelectList();
        Task<IEnumerable<Project>> GetDepartmentProjects(int departmentId);
        Task Create(CreateProjectVM model);
        Task<bool> Update(EditProjectVM model);
        Task<bool> Delete(int id);
    }
}
