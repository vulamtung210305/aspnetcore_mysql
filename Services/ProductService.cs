using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcMovie.Models;
using MvcMovie.Repositories;

namespace MvcMovie.Services;
public class ProductService : IProductService
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IList<KeyValuePair<string, string>>> Validate(ProductDto productDto)
    {
        IList<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
        if (productDto.Price < 0)
            errors.Add(new KeyValuePair<string, string>("Price", "Price cannot be less than zero."));
        var product = await _unitOfWork.Products.GetByName(productDto.Name);
        if (product != null && product.Id != productDto.Id)
        {
            errors.Add(new KeyValuePair<string, string>("Name", "Name is duplicated."));
        }
        return errors;
    }
    public async Task<bool> Save(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        if (product.Id > 0)
        {
            await _unitOfWork.Products.Update(product);
        }
        else
        {
            await _unitOfWork.Products.Insert(product);
        }

        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<ProductDto> GetById(int id)
    {
        var product = await _unitOfWork.Products.GetById(id);
        return _mapper.Map<ProductDto>(product);
    }
}