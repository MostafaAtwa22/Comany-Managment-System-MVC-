using AutoMapper;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Services.ViewModels.Dependents;

namespace Comany_Managment_System_MVC_.Services.DependentServices
{
    public class DependentService : IDependentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly string _imagesPath;

        public DependentService(IUnitOfWork unitOfWork,
            IHostingEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}/Dependents";
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dependent>> GetAll()
        {
            var specification = new DependentWithSpecification();
            var dependents = await _unitOfWork.Dependents.GetAllWithSpecification(specification);
            return dependents
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task<Dependent?> Find(Expression<Func<Dependent, bool>> criteria)
        {
            var specification = new DependentWithSpecification();
            var dependent = await _unitOfWork.Dependents.FindWithSpecification(criteria, specification);
            if (dependent is null)
                return null;
            return dependent;
        }

        public async Task<IEnumerable<Dependent?>> GetEmployeesDependents(int employeeId)
        {
            var specification = new DependentWithSpecification();
            var dependents = await _unitOfWork.Dependents.GetAllWithSpecification(specification);
            return dependents
                .Where(d => d.EmployeeId == employeeId)
                .OrderBy(d => d.Name)
                .ToList();
        }

        public async Task Create(CreateDependentVM model)
        {
            var imageName = await SaveImage(model.Image);

            Dependent dependent = _mapper.Map<Dependent>(model);

            dependent.Image = imageName;

            await _unitOfWork.Dependents.Create(dependent);
        }

        public async Task<Dependent?> Update(EditDependentVM model)
        {
            var dependent = await FindWithTrack(model.Id);

            if (dependent is null)
                return null!;

            var hasNewImage = model.Image is not null;
            var oldImage = dependent.Image;

            _mapper.Map(model, dependent);

            if (hasNewImage)
                dependent.Image = await SaveImage(model.Image!);

            var effectedRows = await _unitOfWork.Dependents.Update(dependent);

            if (effectedRows > 0)
            {
                if (hasNewImage)
                {
                    var cover = Path.Combine(_imagesPath, oldImage);
                    File.Delete(cover);
                }

                return dependent;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, dependent.Image);
                File.Delete(cover);

                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            bool isDeleted = false;
            var dependent = await FindWithTrack(id);

            if (dependent is null)
                return false;

            var effectedRows = await _unitOfWork.Dependents.Delete(dependent);

            if (effectedRows > 0)
            {
                isDeleted = true;

                var image = Path.Combine(_imagesPath, dependent.Image);
                if (File.Exists(image))
                {
                    File.Delete(image);
                }
            }
            return isDeleted;
        }

        private async Task<Dependent?> FindWithTrack(int id)
        {
            var specification = new DependentWithSpecification();
            return await _unitOfWork.Dependents
                .FindWithSpecificationWithTrack(e => e.Id == id, specification);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            var path = Path.Combine(_imagesPath, imageName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream);

            return imageName;
        }
    }
}
