using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Common;
namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        ConcurrentDictionary<string, object> RepoitoriesDic;
        GymDbContext _gymDbContext;
        public UnitOfWork(GymDbContext gymDbContext ) { 
            RepoitoriesDic = new ConcurrentDictionary<string, object>();
            _gymDbContext = gymDbContext;
        }
        public IRepository<T, TKey> GetRepositories<T, TKey>() where T : EntityBase<TKey>
        {
             var repo = RepoitoriesDic.GetOrAdd(typeof(T).Name,_=> new Repository<T, TKey>(_gymDbContext)) as IRepository<T,TKey>;
             if(repo != null) 
                return repo;
            throw new Exception("invalid Casting");            
        }
        public async Task<int> CompleteSaveAsync()
        {
            return await _gymDbContext.SaveChangesAsync();
        }
    }
}
