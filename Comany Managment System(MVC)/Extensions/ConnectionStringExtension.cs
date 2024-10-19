using Comany_Managment_System_MVC_.Repository.Data;
using Comany_Managment_System_MVC_.Repository.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Comany_Managment_System_MVC_.Extensions
{
    public static class ConnectionStringExtension
    {
        public static void AddConnectionStringService(this WebApplicationBuilder builder)
        {
            var constr = builder.Configuration.GetConnectionString("constr")
                    ?? throw new InvalidOperationException("No Connection String");

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(constr)
                .AddInterceptors(new SoftDeleteInterceptor());
            });
        }
    }
}
