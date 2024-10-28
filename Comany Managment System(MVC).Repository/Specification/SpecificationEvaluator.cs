using Comany_Managment_System_MVC_.Core.Spercification;
using Comany_Managment_System_MVC_.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comany_Managment_System_MVC_.Repository.Specification
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecifications<T> specifications)
        {
            var q = query;

            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            query = specifications.Includes.Aggregate(query, (cur, include) =>
                cur.Include(include)
            );

            return query;
        }
    }
}
