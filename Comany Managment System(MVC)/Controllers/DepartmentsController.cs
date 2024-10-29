using Comany_Managment_System_MVC_.Core.Models;

namespace Comany_Managment_System_MVC_.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonDepartmentVM>>> Index()
        {
            var departments = await _departmentService.GetAll();
            return View(departments);
        }

        [HttpGet]
        public async Task<ActionResult<Department>> Details(int id)
        {
            var department = await _departmentService.Find(i => i.Id == id);
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateDepartmentVM model = new CreateDepartmentVM
            {
                ManagersSelectList = await _employeeService.GetUnassignedManagers()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ManagersSelectList = await _employeeService.GetUnassignedManagers();
                return View(model);
            }

            await _departmentService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.Find(d => d.Id == id);

            if (department is null)
                return NotFound();

            EditDepartmentVM model = _mapper.Map<EditDepartmentVM>(department);

            model.ManagersSelectList = await _employeeService.GetManagersForEdit(model.Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ManagersSelectList = await _employeeService.GetManagersForEdit(model.Id);
                return View(model);
            }

            bool find = await _departmentService.Update(model);

            if (!find)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _departmentService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> IsNameUnique(string Name, int? Id)
        {
            var department = await _departmentService.Find(e => e.Name == Name && e.Id != Id);
            bool isUnique = department == null;
            return Json(isUnique);
        }
    }
}
