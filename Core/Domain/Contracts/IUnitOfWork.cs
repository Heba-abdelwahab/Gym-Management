using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<T,TKey> GetRepositories<T,TKey> () where T :EntityBase<TKey>;
        Task<int> CompleteSaveAsync();
    }
}
