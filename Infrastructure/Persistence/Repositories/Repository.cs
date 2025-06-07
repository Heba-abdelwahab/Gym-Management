using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
namespace Persistence.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : EntityBase<TKey>
    {
        private readonly GymDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;   
        public Repository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T, TKey> specification)
        {
           return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdWithSpecAsync(ISpecification<T, TKey> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public void Insert(T newEntity)
        {
            _dbSet.Add(newEntity);
        }

        public void Update(T newEntity)
        {
            _dbSet.Update(newEntity);
        }
        public  void Delete(T entity)
        {
            entity.IsDeleted=true;
            _dbSet.Update(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T,TKey> specification)
        {
             return SpecificationEvaluator.GetQuery(_dbSet, specification);
        }

    }
}
