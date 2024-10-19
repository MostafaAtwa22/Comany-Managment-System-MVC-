using Comany_Managment_System_MVC_.Models;

namespace Comany_Managment_System_MVC_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T model);
        Task Update(T model);
        Task Delete(T model);
        Task Save();
    }
}
