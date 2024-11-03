using Comany_Managment_System_MVC_.Core.Interfaces;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Repository.Data;

namespace Comany_Managment_System_MVC_.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Department> Departments { get; private set; }
        public IGenericRepository<Dependent> Dependents { get; private set; }
        public IGenericRepository<Employee> Employees { get; private set; }
        public IGenericRepository<Project> Projects { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Departments = new GenericRepository<Department>(_context);
            Dependents = new GenericRepository<Dependent>(_context);
            Employees = new GenericRepository<Employee>(_context);
            Projects = new GenericRepository<Project>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
