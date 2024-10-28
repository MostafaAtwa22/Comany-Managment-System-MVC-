using AutoMapper;
using Comany_Managment_System_MVC_.Core.Enums;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Services.ViewModels.Departments;
using Comany_Managment_System_MVC_.Services.ViewModels.Employees;

namespace Comany_Managment_System_MVC_.Services.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            DepartmentMapper();
            EmployeeMapper();
        }

        private void DepartmentMapper()
        {
            CreateMap<CreateDepartmentVM, Department>()
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.ManagerId));

            CreateMap<Department, CreateDepartmentVM>()
                .ForMember(dest => dest.ManagersSelectList, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditDepartmentVM, Department>()
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.ManagerId));

            CreateMap<Department, EditDepartmentVM>()
                .ForMember(dest => dest.ManagersSelectList, opt => opt.Ignore())
                .ReverseMap();
        }

        private void EmployeeMapper()
        {
            CreateMap<CreateEmployeeVM, Employee>()
                        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Gender)src.Gender)) 
                        .ForMember(dest => dest.EmployeeProjects, opt => opt.MapFrom(src => src.SelectedProjects.Select(projectId => new EmployeeProjects
                        {
                            ProjectId = projectId,
                            StartDate = DateTime.Now,
                            Hours = 0
                        })))
                        .ReverseMap();

            CreateMap<EditEmployeeVM, Employee>()
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Gender)src.Gender))
               .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
               .ForMember(dest => dest.EmployeeProjects, opt => opt.MapFrom(src =>
                   src.SelectedProjects.Select(projectId => new EmployeeProjects
                   {
                       ProjectId = projectId,
                       StartDate = DateTime.Now,
                       Hours = 0
                   })))
               .ForMember(dest => dest.Image, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
