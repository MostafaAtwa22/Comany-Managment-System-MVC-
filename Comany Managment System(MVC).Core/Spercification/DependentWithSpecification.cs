using Comany_Managment_System_MVC_.Core.Models;

namespace Comany_Managment_System_MVC_.Core.Spercification
{
    public class DependentWithSpecification : BaseSpecification<Dependent>
    {
        public DependentWithSpecification()
        {
            Includes.Add(d => d.Employee);
        }

        public DependentWithSpecification(int id)
            :base(d => d.Id == id)
        {
            Includes.Add(d => d.Employee);
        }
    }
}
