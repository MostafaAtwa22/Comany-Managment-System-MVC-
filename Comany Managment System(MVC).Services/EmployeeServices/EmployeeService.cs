﻿namespace Comany_Managment_System_MVC_.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly string _imagesPath;

        public EmployeeService(IUnitOfWork unitOfWork,
            IHostingEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}/Employees";
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var specification = new EmployeeWithManagerSpecification();
            var Employees = await _unitOfWork.Employees.GetAllWithSpecification(specification);
            return Employees
                .OrderBy(e => e.Name)
                .ToList();
        }

        public async Task<Employee?> Find(Expression<Func<Employee, bool>> criteria)
        {
            var specification = new EmployeeWithManagerSpecification();
            var employee = await _unitOfWork.Employees.FindWithSpecification(criteria, specification);
            if (employee is null)
                return null;
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetManagers()
        {
            var departments = await _unitOfWork.Departments
                .FindAll(d => d.ManagerId.HasValue);

            var managerIds = departments
                .Select(d => d.ManagerId!.Value)
                .ToList();

            var managers = await _unitOfWork.Employees
                .FindAll(e => managerIds.Contains(e.Id));

            return managers
                .OrderBy(e => e.Name)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetDepartmentEmployees(int departmentId)
        {
            var employees = await _unitOfWork.Employees
            .FindAll(e => e.DepartmentId == departmentId);

            return employees
                .OrderBy(e => e.Name)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetProjectsEmployees(int projectId)
        {
            var project = await _unitOfWork.Projects
                .Find(p => p.Id == projectId);

            var employees = await _unitOfWork.Employees
                .FindAll(e => e.Projects.Contains(project!));

            return employees
                .OrderBy(e => e.Name)
                .ToList();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesUnderManager(int managerId)
        {
            var department = await _unitOfWork.Departments.Find(d => d.ManagerId == managerId);
            var managers = await GetDepartmentEmployees(department!.Id);
            return managers
                .OrderBy(m => m.Name)
                .ToList();
        }

        public async Task<IEnumerable<SelectListItem>> GetUnassignedManagers()
        {
            var assignedManagerIds = (await _unitOfWork.Departments.GetAll())
                .Where(d => d.ManagerId.HasValue)
                .Select(d => d.ManagerId!.Value)
                .ToList();

            return (await _unitOfWork.Employees.GetAll())
                .Where(e => !assignedManagerIds.Contains(e.Id))
                .Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                })
                .OrderBy(d => d.Text)
                .ToList();
        }

        public async Task<IEnumerable<SelectListItem>> GetManagersForEdit(int departmentId)
        {
            var employees = await _unitOfWork.Employees
                .FindAll(e => e.DepartmentId == departmentId);

            var selectListItems = employees.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            })
            .OrderBy(e => e.Text)
            .ToList();

            return selectListItems;
        }

        public async Task Create(CreateEmployeeVM model)
        {
            var imageName = await SaveImage(model.Image);

            Employee employee = _mapper.Map<Employee>(model);

            employee.Image = imageName;

            await _unitOfWork.Employees.Create(employee);
        }

        public async Task<Employee?> Update(EditEmployeeVM model)
        {
            var employee = await FindWithTrack(model.Id);

            if (employee is null)
                return null!;

            var hasNewImage = model.Image is not null;
            var oldImage = employee.Image;

            _mapper.Map(model, employee);

            if (hasNewImage)
                employee.Image = await SaveImage(model.Image!);

            var effectedRows = await _unitOfWork.Employees.Update(employee);

            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagesPath, oldImage);
                    File.Delete(cover);
                }

                return employee;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, employee.Image);
                File.Delete(cover);

                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            bool isDeleted = false;
            var employee = await FindWithTrack(id);

            if (employee is null)
                return false;

            var effectedRows = await _unitOfWork.Employees.Delete(employee);

            if (effectedRows > 0)
            {
                isDeleted = true;

                var image = Path.Combine(_imagesPath, employee.Image);
                if (File.Exists(image))
                {
                    File.Delete(image);
                }
            }
            return isDeleted;
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            var path = Path.Combine(_imagesPath, imageName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream);

            return imageName;
        }

        private async Task<Employee?> FindWithTrack(int id)
        {
            var specification = new EmployeeWithManagerSpecification();
            return await _unitOfWork.Employees
                .FindWithSpecificationWithTrack(e => e.Id == id, specification);
        }
    }
}
