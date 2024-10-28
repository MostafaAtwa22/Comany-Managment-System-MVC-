using Comany_Managment_System_MVC_.Core.Models;

namespace Comany_Managment_System_MVC_.Core.Spercification
{
    public class EmployeeWithManagerSpecification : BaseSpecification<Employee>
    {
        public EmployeeWithManagerSpecification()
        {
            Includes.Add(d => d.Projects);
            Includes.Add(d => d.Department);
        }

        public EmployeeWithManagerSpecification(int id)
            :base(d => d.Id == id)
        {
            Includes.Add(d => d.Projects);
            Includes.Add(d => d.Department);
        }
    }
}
