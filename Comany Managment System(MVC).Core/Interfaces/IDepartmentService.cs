using Comany_Managment_System_MVC_.Models;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> Find(Expression<Func<Department, bool>> criteria);
        Task Create(Department model);
        Task Update(Department model);
        Task Delete(Department model);
    }
}
