using AutoMapper;
using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Models;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Department>> GetAll()
        {
            var specification = new DepartmentWithManagerSpecification();
            var departmetnts = await _unitOfWork.Departments.GetAllWithSpecification(specification);
            return departmetnts;
        }

        public async Task<Department?> Find(Expression<Func<Department, bool>> criteria)
        {
            var specification = new DepartmentWithManagerSpecification();
            var department = await _unitOfWork.Departments.FindWithSpecification(criteria, specification);
            if (department is null)
                return null;
            return department;
        }

        public async Task Create(Department model)
            => await _unitOfWork.Departments.Create(model);

        public async Task Update(Department model)
            => await _unitOfWork.Departments.Update(model); 

        public async Task Delete(Department model)
            => await _unitOfWork.Departments.Delete(model);
    }
}
