using Comany_Managment_System_MVC_.Repository.Repositories;
using Comany_Managment_System_MVC_.Services.DependentServices;
using Comany_Managment_System_MVC_.Services.ProjectServices;

namespace Comany_Managment_System_MVC_.Extensions
{
    public static class ApplicationRepoitoriesExtension
    {
        public static void AddRepoitoriesServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IDependentService, DependentService>();        
        }
    }
}
