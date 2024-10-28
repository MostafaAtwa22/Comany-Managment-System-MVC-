using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Services.ViewModels.Employees;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<SelectListItem>> GetUnassignedManagers();
        Task<IEnumerable<SelectListItem>> GetManagersForEdit(int departmentId);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee?> Find(Expression<Func<Employee, bool>> criteria);
        Task Create(CreateEmployeeVM model);
        Task<Employee?> Update(EditEmployeeVM model);
        Task<bool> Delete(int id);
    }
}
