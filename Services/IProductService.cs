using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcMovie.Models;

namespace MvcMovie.Services;
public interface IProductService
{
    Task<IList<KeyValuePair<string, string>>> Validate(ProductDto product);
    Task<bool> Save(ProductDto product);
    Task<ProductDto> GetById(int id);
}