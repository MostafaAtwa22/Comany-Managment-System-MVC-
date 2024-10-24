using Comany_Managment_System_MVC_.Core.Interfaces;
using Comany_Managment_System_MVC_.Repository.Repositories;
using Comany_Managment_System_MVC_.Services.DepartmentServices;

namespace Comany_Managment_System_MVC_.Extensions
{
    public static class ApplicationRepoitoriesExtension
    {
        public static void AddRepoitoriesServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }
    }
}
