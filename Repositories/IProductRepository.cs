using MvcMovie.Models;

namespace MvcMovie.Repositories;
public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product?> GetByName(string name);
}