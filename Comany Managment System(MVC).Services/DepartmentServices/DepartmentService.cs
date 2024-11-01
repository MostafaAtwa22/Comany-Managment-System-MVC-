using AutoMapper;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Services.ViewModels.Departments;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task<IEnumerable<SelectListItem>> GetSelectList()
        {
            var departments = await _unitOfWork.Departments.GetAll();
            return departments
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
                .OrderBy(d => d.Text)
                .ToList();
        }

        public async Task Create(CreateDepartmentVM model)
        {
            Department department = _mapper.Map<Department>(model);

            await _unitOfWork.Departments.Create(department);

            if (department.ManagerId is null)
                return;

            var specification = new EmployeeWithManagerSpecification();
            var employee = await _unitOfWork.Employees
                .FindWithSpecificationWithTrack(d => d.Id == model.ManagerId, specification);

            employee!.DepartmentId = department.Id;

            await _unitOfWork.Employees.Update(employee);
        }

        public async Task<bool> Update(EditDepartmentVM model)
        {
            var dept = await FindWithTrack(model.Id);

            if (dept is null)
                return false;

            _mapper.Map(model, dept);

            var deptManager = await UpdateManager(model.ManagerId);

            if (deptManager is not null)
            {
                deptManager.ManagerId = null;
                await _unitOfWork.Departments.Update(deptManager);
            }
            await _unitOfWork.Departments.Update(dept);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var dept = await FindWithTrack(id);

            if (dept is null)
                return false;

            await _unitOfWork.Departments.Delete(dept);
            return true;
        }

        private async Task<Department?> FindWithTrack(int id)
        {
            var specification = new DepartmentWithManagerSpecification();
            return await _unitOfWork.Departments
                .FindWithSpecificationWithTrack(d => d.Id == id, specification);
        }

        private async Task<Department?> UpdateManager(int? managerId)
        {
            var specification = new DepartmentWithManagerSpecification();
            return await _unitOfWork.Departments
                .FindWithSpecificationWithTrack(d => d.ManagerId == managerId, specification);
        }
    }
}
