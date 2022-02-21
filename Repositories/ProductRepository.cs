using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(DataContext context, ILogger logger) : base(context, logger) { }
    public override async Task<bool> Update(Product entity)
    {
        try
        {
            dbSet.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Repo} Upsert function error", typeof(ProductRepository));
            return false;
        }
    }

    public async Task<Product?> GetByName(string name)
    {
        return await dbSet.AsNoTracking().FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
    }
}