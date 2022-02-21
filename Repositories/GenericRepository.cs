using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DataContext _context;
    internal DbSet<T> dbSet;
    public readonly ILogger _logger;

    public GenericRepository(
        DataContext context,
        ILogger logger)
    {
        _context = context;
        this.dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<T> GetById(int id)
    {
        var entity = await dbSet.FindAsync(id);
        _context.Entry<T>(entity).State = EntityState.Detached;
        return entity;
    }

    public virtual async Task<bool> Insert(T entity)
    {
        await dbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<bool> Delete(int id)
    {
        var entity = await GetById(id);
        dbSet.Remove(entity);
        return true;
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        return await dbSet.ToListAsync<T>();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<bool> Update(T entity)
    {
        dbSet.Update(entity);
        return true;
    }
}