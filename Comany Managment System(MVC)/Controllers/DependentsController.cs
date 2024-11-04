using Comany_Managment_System_MVC_.Core.Enums;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Services.DependentServices;
using Comany_Managment_System_MVC_.Services.ProjectServices;
using Company_Management_System_MVC_.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Comany_Managment_System_MVC_.Controllers
{
    public class DependentsController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDependentService _dependentService;
        private readonly IMapper _mapper;

        public DependentsController(IEmployeeService employeeService,
            IDependentService dependentService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _dependentService = dependentService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependent>>> Index()
        {
            var dependents = await _dependentService.GetAll();
            return View(dependents);
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet]
        public async Task<ActionResult<Dependent>> Details(int id)
        {
            var dependent = await _dependentService.Find(e => e.Id == id);

            if (dependent is null)
                return NotFound();

            return View(dependent);
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependent?>>> GetEmployeesDependents(int id)
        {
            var dependents = await _dependentService.GetEmployeesDependents(id);
            return View("Index", dependents);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create(int id)
        {
            CreateDependentVM model = new CreateDependentVM
            {
                EmployeeId = id,
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
                            })).ToList(),
                Relation = (Relation)(-1),
                RelaionSelectList = new List<SelectListItem>
                        {
                            new SelectListItem { Value = "", Text = "Select a Relation", Selected = true }
                        }
                        .Concat(Enum.GetValues(typeof(Relation)).Cast<Relation>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            })).ToList(),
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDependentVM model, int id)
        {
            if (!ModelState.IsValid)
            {
                model.EmployeeId = id;
                model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList();

                model.RelaionSelectList = Enum.GetValues(typeof(Relation)).Cast<Relation>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList();
            }

            model.EmployeeId = id;

            await _dependentService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dependent = await _dependentService.Find(e => e.Id == id);

            if (dependent is null)
                return NotFound();

            EditDependentVM model = _mapper.Map<EditDependentVM>(dependent);

            model.CurrentImage = dependent.Image;
            model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                }).ToList();

            model.RelaionSelectList = Enum.GetValues(typeof(Relation)).Cast<Relation>()
                .Select(r => new SelectListItem
                {
                    Value = ((int)r).ToString(),
                    Text = r.ToString()
                }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDependentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList();

                model.RelaionSelectList = Enum.GetValues(typeof(Relation)).Cast<Relation>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList();
                return View(model);
            }

            var emp = await _dependentService.Update(model);

            if (emp is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _dependentService.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
