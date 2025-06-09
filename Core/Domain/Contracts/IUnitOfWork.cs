using Domain.Common;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<T, TKey> GetRepositories<T, TKey>() where T : EntityBase<TKey>;
        Task<bool> CompleteSaveAsync();
    }
}
