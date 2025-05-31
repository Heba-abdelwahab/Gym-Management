using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public class SpecificationBase<T, TKey> : ISpecification<T, TKey> where T : EntityBase<TKey>
    {
        public SpecificationBase() { 
        }
        public SpecificationBase(Expression<Func<T,bool>>_criteria)
        {
            Criteria = _criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; private set; } = null!;

        public List<Expression<Func<T, object>>> Includes { get; private set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; } = null!;

        public Expression<Func<T, object>> OrderByDesending { get; private set; } = null!;

        public int? Take { get; private set; } =null!;   

        public int? Skip { get; private set; } = null!;

        public void AddIclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        public void ApplyOrderBy(Expression<Func<T, object>> orderBy) {
            OrderBy = orderBy;
        }
        public void ApplyOrderByDesending(Expression<Func<T, object>> orderByDes)
        {
            OrderByDesending = orderByDes;
        }
        public void ApplyTake(int take) {  Take = take; }
        public void ApplySkip(int skip) { Skip = skip; }
    }
}
