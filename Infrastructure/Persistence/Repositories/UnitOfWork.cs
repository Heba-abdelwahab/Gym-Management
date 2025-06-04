using Domain.Common;
using Domain.Contracts;
using System.Collections.Concurrent;
namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        ConcurrentDictionary<string, object> RepoitoriesDic;
        GymDbContext _gymDbContext;
        public UnitOfWork(GymDbContext gymDbContext)
        {
            RepoitoriesDic = new ConcurrentDictionary<string, object>();
            _gymDbContext = gymDbContext;
        }
        public IRepository<T, TKey> GetRepositories<T, TKey>() where T : EntityBase<TKey>
        {
            var repo = RepoitoriesDic.GetOrAdd(typeof(T).Name, _ => new Repository<T, TKey>(_gymDbContext)) as IRepository<T, TKey>;
            if (repo != null)
                return repo;
            throw new Exception("invalid Casting");
        }
        public async Task<bool> CompleteSaveAsync()
        {
            return await _gymDbContext.SaveChangesAsync() > 0;
        }
    }
}
