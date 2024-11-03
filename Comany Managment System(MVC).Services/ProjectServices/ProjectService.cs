using AutoMapper;
using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Services.ViewModels.Projects;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var specification = new ProjectWithSpecification();
            var projects = await _unitOfWork.Projects.GetAllWithSpecification(specification);
            return projects
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task<Project?> Find(Expression<Func<Project, bool>> criteria)
        {
            var specification = new ProjectWithSpecification();
            var project = await _unitOfWork.Projects.FindWithSpecification(criteria, specification);
            return project ?? null;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectList()
        {
            var Projects = await _unitOfWork.Projects.GetAll();

            return Projects
                .Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() })
                .OrderBy(d => d.Text) 
                .ToList();
        }

        public async Task<IEnumerable<Project>> GetDepartmentProjects(int departmentId)
        {
            var projects = await _unitOfWork.Projects
            .FindAll(d => d.DepartmentId == departmentId);

            return projects
                .OrderBy(p => p.Name)
                .ToList();
        }

        public async Task Create(CreateProjectVM model)
        {
            Project project = _mapper.Map<Project>(model);
            await _unitOfWork.Projects.Create(project);
        }

        public async Task<bool> Update(EditProjectVM model)
        {
            var project = await FindWithTrack(model.Id);

            if (project is null)
                return false;

            _mapper.Map(model, project);

            await _unitOfWork.Projects.Update(project); 
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var project = await FindWithTrack(id);

            if (project is null)
                return false;

            await _unitOfWork.Projects.Delete(project);
            return true;
        }

        private async Task<Project?> FindWithTrack(int id)
        {
            var specification = new ProjectWithSpecification();
            return await _unitOfWork.Projects
                .FindWithSpecificationWithTrack(d => d.Id == id, specification);
        }
    }
}
