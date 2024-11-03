using Comany_Managment_System_MVC_.Core.Models;

namespace Comany_Managment_System_MVC_.Core.Spercification
{
    public class ProjectWithSpecification : BaseSpecification<Project>
    {
        public ProjectWithSpecification()
        {
            Includes.Add(d => d.Department);
        }

        public ProjectWithSpecification(int id)
            :base(p => p.Id == id)
        {
            Includes.Add(d => d.Department);
        }
    }
}
