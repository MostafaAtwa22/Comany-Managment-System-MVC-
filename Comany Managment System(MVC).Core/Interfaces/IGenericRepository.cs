using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Core.Models;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithSpecification(ISpecifications<T> specifications);
        Task<T?> Find(Expression<Func<T, bool>> criteria);
        Task<T?> FindWithSpecification(Expression<Func<T, bool>> criteria, ISpecifications<T> specifications);
        Task<T?> FindWithSpecificationWithTrack(Expression<Func<T, bool>> criteria, ISpecifications<T> specifications);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria);
        Task Create(T model);
        Task<int> Update(T model);
        Task<int> Delete(T model);
    }
}
