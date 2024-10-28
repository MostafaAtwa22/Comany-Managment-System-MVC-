using Comany_Managment_System_MVC_.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comany_Managment_System_MVC_.Core.Spercification
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } 

        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}
