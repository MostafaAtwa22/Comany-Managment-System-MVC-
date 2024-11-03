using Comany_Managment_System_MVC_.Services.ViewModels.Dependents;
using Comany_Managment_System_MVC_.Services.ViewModels.Employees;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.DependentServices
{
    public interface IDependentService
    {
        Task<IEnumerable<Dependent>> GetAll();
        Task<Dependent?> Find(Expression<Func<Dependent, bool>> criteria);
        Task<IEnumerable<Dependent?>> GetEmployeesDependents(int employeeId);
        Task Create(CreateDependentVM model);
        Task<Dependent?> Update(EditDependentVM model);
        Task<bool> Delete(int id);
    }
}
