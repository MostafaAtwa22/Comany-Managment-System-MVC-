using Comany_Managment_System_MVC_.Services.ViewModels.Projects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Comany_Managment_System_MVC_.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task<IEnumerable<SelectListItem>> GetSelectList()
        {
            var Projects = await _unitOfWork.Projects.GetAll();

            return Projects
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
                .OrderBy(d => d.Text) 
                .ToList();
        }
    }
}
