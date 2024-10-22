using Comany_Managment_System_MVC_.Models;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Find(Expression<Func<T, bool>> criteria);
        Task Create(T model);
        Task Update(T model);
        Task Delete(T model);
    }
}
