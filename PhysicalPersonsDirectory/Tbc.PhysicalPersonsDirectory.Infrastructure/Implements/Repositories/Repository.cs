using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tbc.PhysicalPersonsDirectory.Domain.Abstracts;
using Tbc.PhysicalPersonsDirectory.Domain.Repositories;
using Tbc.PhysicalPersonsDirectory.Persistence.Contexts;

namespace Tbc.PhysicalPersonsDirectory.Infrastructure.Implements.Repositories;

public class Repository<T> : IRepository<T> where T : Entity<int>
{
    private readonly DbSet<T> _dbSet;

    public Repository(PhysicalPersonsContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> GetByIdIncludedDataAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null)
    {
        var query = _dbSet.AsQueryable();

        if (include != null)
        {
            query = include(query); // Dynamically apply the Include (and ThenInclude if necessary)
        }

        var result = await query.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    private static Expression<Func<T, object>> BuildIncludeExpression(string navigationProperty)
    {
        if (string.IsNullOrWhiteSpace(navigationProperty))
            return null;

        // Use reflection to get the property info based on the provided navigation property name
        var propertyInfo = typeof(T).GetProperty(navigationProperty);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"Property '{navigationProperty}' not found on type {typeof(T).Name}");
        }

        // Build the expression for the Include
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda<Func<T, object>>(property, parameter);

        return lambda;
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}