using Comany_Managment_System_MVC_.Core.Enums;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Services.DepartmentServices;
using Comany_Managment_System_MVC_.Services.EmployeeServices;
using Comany_Managment_System_MVC_.Services.ProjectServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace Comany_Managment_System_MVC_.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, 
            IDepartmentService departmentService,
            IProjectService projectService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Index()
        {
            var employees = await _employeeService.GetAll();
            return View(employees);
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet]
        public async Task<ActionResult<Employee>> Details(int id)
        {
            var employee = await _employeeService.Find(e => e.Id == id);

            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetManagers()
        {
            var employees = await _employeeService.GetManagers();

            return View(employees);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesUnderManager(int id)
        {
            var employees = await _employeeService.GetEmployeesUnderManager(id);

            return View("GetManagers", employees);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateEmployeeVM model = new CreateEmployeeVM
            {
                Departments = await _departmentService.GetSelectList(),
                Projects = await _projectService.GetSelectList(),
                Gender = -1,
                GenderSelectList = new List<SelectListItem>
                        {
                            new SelectListItem { Value = "", Text = "Select a Gender", Selected = true }
                        }
                        .Concat(Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            })).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _departmentService.GetSelectList();
                model.Projects = await _projectService.GetSelectList();
                model.GenderSelectList = new List<SelectListItem>
                        {
                            new SelectListItem { Value = "", Text = "Select Gender", Selected = true }
                        }
                        .Concat(Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            })).ToList();
                return View(model);
            }

            await _employeeService.Create(model); 

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.Find(e => e.Id == id);

            if (employee is null)
                return NotFound();

            EditEmployeeVM model = new EditEmployeeVM
            {
                Id = id,
                Name = employee.Name,
                Email = employee.Email,
                Address = employee.Address,
                CurrentImage = employee.Image,
                DepartmentId = employee.DepartmentId,
                JobTitle = employee.JobTitle,
                Age = employee.Age,
                Salary = employee.Salary,
                Gender = (int)employee.Gender, 
                Projects = await _projectService.GetSelectList(),
                Departments = await _departmentService.GetSelectList(),
                SelectedProjects = employee.Projects.Select(p => p.Id).ToList()
            };

            model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString(),
                    Selected = g == employee.Gender 
                }).ToList();

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Projects = await _projectService.GetSelectList();
                model.Departments = await _departmentService.GetSelectList();
                model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList();
                return View(model);
            }

            var emp = await _employeeService.Update(model);

            if (emp is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _employeeService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesPerDepartment(int deptId)
        {
            var employees = await _employeeService.GetDepartmentEmployees(deptId);

            var employeeData = employees.Select(e => new {
                Name = e.Name,
                Age = e.Age,
                Salary = e.Salary,
                Image = e.Image,
                StartDate = e.StartDate,
            }).ToList();

            return Json(employeeData);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesPerProject(int projectId)
        {
            var employees = await _employeeService.GetProjectsEmployees(projectId);

            var employeeData = employees.Select(e => new {
                Name = e.Name,
                Age = e.Age,
                Salary = e.Salary,
                Image = e.Image
            }).ToList();

            return Json(employeeData);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> IsEmailUnique(string Email, int? Id)
        {
            var employee = await _employeeService.Find(e => e.Email == Email && e.Id != Id);
            bool isUnique = employee == null;
            return Json(isUnique);
        }
    }
}
