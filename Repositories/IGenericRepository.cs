using System.Linq.Expressions;

namespace MvcMovie.Repositories;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> All();
    Task<T> GetById(int id);
    Task<bool> Insert(T entity);
    Task<bool> Delete(int id);
    Task<bool> Update(T entity);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
}