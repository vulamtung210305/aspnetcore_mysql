using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcMovie.Models;

namespace MvcMovie.Services;
public interface IOrderService
{
    Task<IList<KeyValuePair<string, string>>> Validate(OrderDto orderDto);
    Task<bool> Save(OrderDto orderDto);
    Task<ProductDto> GetById(int id);
}