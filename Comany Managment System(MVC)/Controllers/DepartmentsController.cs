using AutoMapper;
using Comany_Managment_System_MVC_.Core.Interfaces;
using Comany_Managment_System_MVC_.Models;
using Comany_Managment_System_MVC_.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comany_Managment_System_MVC_.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService,
            IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Index()
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
    }
}
