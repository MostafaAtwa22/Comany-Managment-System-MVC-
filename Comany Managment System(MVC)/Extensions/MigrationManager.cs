using Comany_Managment_System_MVC_.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Comany_Managment_System_MVC_.Extensions
{
    public static class MigrationManager
    {
        public static async Task ApplyMigrationsAsync(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dbContext = services.GetRequiredService<ApplicationDbContext>(); 
                await dbContext.Database.MigrateAsync(); 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while applying migrations.", ex);
            }
        }
    }
}
