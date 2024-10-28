using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Services.ViewModels.Departments;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<SelectListItem>> GetSelectList();
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> Find(Expression<Func<Department, bool>> criteria);
        Task Create(CreateDepartmentVM model);
        Task<bool> Update(EditDepartmentVM model);
        Task<bool> Delete(int id);
    }
}
