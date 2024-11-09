using Tbc.PhysicalPersonsDirectory.Domain.Abstracts;

namespace Tbc.PhysicalPersonsDirectory.Domain.Repositories
{
    public interface IBaseRepository<T, TKey> where T : Entity<TKey>
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        Task<T> GetByIdAsync(TKey id);
    }

    public interface IRepository<T> : IBaseRepository<T, int> where T : Entity<int>
    {
        Task<T> GetByIdIncludedDataAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null);

        IQueryable<T> GetQueryable();
    }
}