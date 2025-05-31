using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T,TKey>(IQueryable<T>query, ISpecification<T,TKey> specification) where T :EntityBase<TKey>
        {
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification.Includes!=null && specification.Includes.Count > 0)
            {
                foreach (var include in specification.Includes) 
                    query = query.Include(include);               
            }
            if(specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            else if(specification.OrderByDesending != null)
                query = query.OrderByDescending(specification.OrderByDesending);

            if (specification.Take != null)
                query = query.Take(specification.Take.Value);

            if(specification.Skip != null)
                query = query.Skip(specification.Skip.Value);

            return query;
        }
    }
}
