using Comany_Managment_System_MVC_.Core.Models;
using Comany_Managment_System_MVC_.Repository.Data;
using Microsoft.AspNetCore.Identity;

namespace Comany_Managment_System_MVC_.Extensions
{
    public static class IdentityExtension
    {
        public static void AddIdentities (this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
