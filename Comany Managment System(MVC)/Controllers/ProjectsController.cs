using Comany_Managment_System_MVC_.Services.ProjectServices;
using Microsoft.AspNetCore.Mvc;

namespace Comany_Managment_System_MVC_.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public ProjectsController(IDepartmentService departmentService,
            IProjectService projectService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> Index()
        {
            var projects = await _projectService.GetAll();
            return View(projects);
        }

        [HttpGet]
        public async Task<ActionResult<Project>> Details(int id)
        {
            var project = await _projectService.Find(i => i.Id == id);
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateProjectVM model = new CreateProjectVM
            {
                Departments = await _departmentService.GetSelectList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _departmentService.GetSelectList();
                return View(model);
            }

            await _projectService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.Find(d => d.Id == id);

            if (project is null)
                return NotFound();

            EditProjectVM model = _mapper.Map<EditProjectVM>(project);

            model.Departments = await _departmentService.GetSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProjectVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _departmentService.GetSelectList();
                return View(model);
            }

            bool find = await _projectService.Update(model);

            if (!find)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _projectService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> GetProjectsPerDepartment(int deptId)
        {
            var projects = await _projectService.GetDepartmentProjects(deptId);

            var projectData = projects.Select(p => new {
                Id = p.Id,
                Name = p.Name,
                Budget = p.Budget,
                Location = p.Location
            }).ToList();

            return Json(projectData);
        }

        [HttpGet]
        public async Task<IActionResult> IsNameUnique(string name, int? id, int DepartmentId)
        {
            var project = await _projectService
                .Find(e => e.Name == name && e.DepartmentId == DepartmentId && e.Id != id);

            bool isUnique = project == null; 
            return Json(isUnique);
        }

    }
}
