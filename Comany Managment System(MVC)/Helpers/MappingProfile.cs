using AutoMapper;
using Comany_Managment_System_MVC_.Models;
using Comany_Managment_System_MVC_.ViewModels.Department;

namespace Comany_Managment_System_MVC_.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, CommonDepartmentVM>()
                .ForMember(d => d.ManagersSelectList, d => d.MapFrom(e => e.ManagerId))
                .ReverseMap();
        }
    }
}
