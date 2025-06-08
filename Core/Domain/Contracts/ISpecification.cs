using Domain.Common;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecification<T, TKey> where T : EntityBase<TKey>
    {
        Expression<Func<T, bool>> Criteria { get; }
        IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
        IReadOnlyList<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int? Take { get; }
        int? Skip { get; }

    }
}
