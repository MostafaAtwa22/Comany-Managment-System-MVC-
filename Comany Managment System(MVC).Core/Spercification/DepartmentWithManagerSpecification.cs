using Comany_Managment_System_MVC_.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comany_Managment_System_MVC_.Core.Spercification
{
    public class DepartmentWithManagerSpecification : BaseSpecification<Department>
    {
        public DepartmentWithManagerSpecification()
        {
            Includes.Add(d => d.Manager!);
        }

        public DepartmentWithManagerSpecification(int id)
            :base(d => d.Id == id)
        {
            Includes.Add(d => d.Manager!);
        }
    }
}
