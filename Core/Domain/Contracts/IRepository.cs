using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Contracts
{
    public interface IRepository<T,TKey> where T:EntityBase<TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T,TKey>specification);

        Task<T?> GetByIdAsync(TKey id);
        Task<T?> GetByIdWithSpecAsync(ISpecification<T,TKey>specification);

        void Insert(T newEntity);
        void Update(T newEntity);
        void Delete(T entity);
    }
}
