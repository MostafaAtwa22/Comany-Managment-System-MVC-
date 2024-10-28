using Comany_Managment_System_MVC_.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comany_Managment_System_MVC_.Repository.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity<EmployeeProjects>(
                    l => l.HasOne(ep => ep.Employee)
                    .WithMany(e => e.EmployeeProjects)
                    .HasForeignKey(ep => ep.EmployeeId),

                    l => l.HasOne(ep => ep.Project)
                    .WithMany(p => p.EmployeeProjects)
                    .HasForeignKey(ep => ep.ProjectId)
                );

            builder.HasOne(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DepartmentId)
                .IsRequired();

            builder.Property(e => e.DateOFDelete)
            .HasColumnType("DATE")
            .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
