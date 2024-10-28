using Comany_Managment_System_MVC_.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Comany_Managment_System_MVC_.Repository.Configurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProjects>
    {
        public void Configure(EntityTypeBuilder<EmployeeProjects> builder)
        {
            builder.HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder.Property(e => e.StartDate)
                .HasColumnType("DATE")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
