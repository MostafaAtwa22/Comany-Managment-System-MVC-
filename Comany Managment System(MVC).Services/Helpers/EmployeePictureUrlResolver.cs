using AutoMapper;
using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Services.ViewModels.Employees;
using Microsoft.Extensions.Configuration;

namespace Comany_Managment_System_MVC_.Services.Helpers
{
    public class EmployeePictureUrlResolver : IValueResolver<Employee, CreateEmployeeVM, string>
    {
        private readonly IConfiguration _configuration;

        public EmployeePictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Employee source, CreateEmployeeVM destination, string destMember, ResolutionContext context)
        {
            return !string.IsNullOrEmpty(source.Image)
                ? $"{_configuration["APIBaseURL"]}{source.Image}"
                : string.Empty;
        }
    }
}
