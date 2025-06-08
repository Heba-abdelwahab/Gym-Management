using Domain.Common;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T, TKey>(IQueryable<T> inputQuery,
            ISpecification<T, TKey> spec) where T : EntityBase<TKey>
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);


            if (spec.Includes is not null)
                query = spec.Includes.Aggregate(query, (current, expressions) => current.Include(expressions));

            if (spec.IncludeStrings is not null)
                query = spec.IncludeStrings.Aggregate(query, (current, stringInclude) => current.Include(stringInclude));


            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.Take is not null)
                query = query.Take(spec.Take.Value);

            if (spec.Skip is not null)
                query = query.Skip(spec.Skip.Value);

            return query;
        }
    }
}
