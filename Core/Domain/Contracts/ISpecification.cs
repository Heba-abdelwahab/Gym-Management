using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecification<T, TKey> where T : EntityBase<TKey>
    {
        Expression<Func<T,bool>>Criteria {  get; }
        List< Expression<Func<T,object>> >Includes { get; }
        Expression<Func<T,object>>OrderBy { get; }
        Expression<Func<T, object>> OrderByDesending { get; }
        int? Take { get; }
        int? Skip { get; }

    }
}
