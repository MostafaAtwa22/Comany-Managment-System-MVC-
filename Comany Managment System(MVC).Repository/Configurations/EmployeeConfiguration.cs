using Comany_Managment_System_MVC_.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Comany_Managment_System_MVC_.Core.Enums;

namespace Comany_Managment_System_MVC_.Repository.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Salary)
                .HasColumnType("MONEY");

            builder.Property(e => e.Image)
                .IsRequired(false);

            builder.Property(e => e.StartDate)
                .HasColumnType("DATE")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.DateOFDelete)
                .HasColumnType("DATE")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);

            builder.Property(e => e.Gender)
                .HasConversion(
                    e => e.ToString(),
                    e => (Gender)Enum.Parse(typeof(Gender), e)
                );

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
