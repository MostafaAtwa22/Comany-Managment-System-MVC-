using Comany_Managment_System_MVC_.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comany_Managment_System_MVC_.Repository.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasOne(d => d.Manager)
                .WithOne(e => e.DepartmentManager)
                .HasForeignKey<Department>(d => d.ManagerId)
                .IsRequired(false);
        }
    }
}
