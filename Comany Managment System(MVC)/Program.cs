using Comany_Managment_System_MVC_.Extensions;
using Comany_Managment_System_MVC_.Services.Helpers;

namespace Comany_Managment_System_MVC_
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.AddConnectionStringService();

            builder.Services.AddRepoitoriesServices();

            builder.Services.AddIdentities();
            
            builder.Services.Configure<MailSettingsVM>(builder.Configuration.GetSection("MailSettings"));
            
            builder.Services.AddTransient<IEmailSettings, EmailSettings>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            await app.ApplyMigrationsAsync();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
