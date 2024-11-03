using Comany_Managment_System_MVC_.Core.Models;

namespace Comany_Managment_System_MVC_.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Department> Departments{ get; }
        public IGenericRepository<Dependent> Dependents { get; }
        public IGenericRepository<Employee> Employees { get; }
        public IGenericRepository<Project> Projects { get; }
    }
}
