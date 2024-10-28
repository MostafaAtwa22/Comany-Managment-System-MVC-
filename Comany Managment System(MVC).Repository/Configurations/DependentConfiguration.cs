using Comany_Managment_System_MVC_.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Comany_Managment_System_MVC_.Core.Enums;

namespace Comany_Managment_System_MVC_.Repository.Configurations
{
    public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.HasKey(d => new { d.Name, d.EmployeeId });

            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Employee)
                .WithMany(e => e.Dependents)
                .HasForeignKey(d => d.EmployeeId)
                .IsRequired(true);

            builder.Property(e => e.Gender)
                .HasConversion(
                    e => e.ToString(),
                    e => (Gender)Enum.Parse(typeof(Gender), e)
                );
        }
    }
}
