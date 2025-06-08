using Domain.Common;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public class SpecificationBase<T, TKey> : ISpecification<T, TKey> where T : EntityBase<TKey>
    {
        protected SpecificationBase() { }
        protected SpecificationBase(Expression<Func<T, bool>> _criteria)
        {
            Criteria = _criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; } = null!;

        private readonly List<Expression<Func<T, object>>> _includes = new();
        private readonly List<string> _includeStrings = new();

        public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes.AsReadOnly();
        public IReadOnlyList<string> IncludeStrings => _includeStrings.AsReadOnly();


        public Expression<Func<T, object>> OrderBy { get; private set; } = null!;

        public Expression<Func<T, object>> OrderByDescending { get; private set; } = null!;

        public int? Take { get; private set; } = null!;

        public int? Skip { get; private set; } = null!;

        protected void AddIncludes(Expression<Func<T, object>> include)
            => _includes.Add(include);

        protected void AddIncludes(string includeString)
          => _includeStrings.Add(includeString);


        protected void ApplyOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDes)
            => OrderByDescending = orderByDes;

        protected void ApplyTake(int take)
            => Take = take;

        protected void ApplySkip(int skip)
            => Skip = skip;
    }
}
