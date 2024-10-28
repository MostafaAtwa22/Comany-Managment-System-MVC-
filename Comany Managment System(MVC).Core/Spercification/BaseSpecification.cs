using Comany_Managment_System_MVC_.Core.Models;
using System.Linq.Expressions;

namespace Comany_Managment_System_MVC_.Core.Spercification
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    }
}
