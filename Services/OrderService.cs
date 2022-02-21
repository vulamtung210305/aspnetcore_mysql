using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcMovie.Models;
using MvcMovie.Repositories;

namespace MvcMovie.Services;
public class OrderService : IOrderService
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IList<KeyValuePair<string, string>>> Validate(OrderDto orderDto)
    {
        IList<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
        // if (productDto.Price < 0)
        //     errors.Add(new KeyValuePair<string, string>("Price", "Price cannot be less than zero."));
        // var product = await _unitOfWork.Products.GetByName(productDto.Name);
        // if (product != null && product.Id != productDto.Id)
        // {
        //     errors.Add(new KeyValuePair<string, string>("Name", "Name is duplicated."));
        // }
        return errors;
    }
    public async Task<bool> Save(OrderDto orderDto)
    {
        var customerDto = orderDto.Customer;
        var customer = _mapper.Map<Customer>(customerDto);
        var orderItemDtos = orderDto.OrderItems;
        var orderItems = _mapper.Map<IList<OrderItem>>(orderItemDtos);
        var order = _mapper.Map<Order>(orderDto);
        order.GrossAmount = orderItemDtos.Sum(m => m.Quantity * m.Price);
        order.TaxAmount = orderItemDtos.Sum(m => m.Quantity * m.Price * 10 / 100);
        order.DueAmount = order.GrossAmount + order.TaxAmount;
        orderItems = orderItems.Select(m =>
        {
            m.GrossAmount = m.Quantity * m.Price;
            m.TaxAmount = m.GrossAmount * 10 / 100;
            m.DueAmount = m.GrossAmount + m.TaxAmount;
            return m;
        }).ToList();
        if (order.Id > 0)
        {
            await _unitOfWork.Orders.Update(order);
        }
        else
        {
            await _unitOfWork.Customers.Insert(customer);
            order.CustomerId = customer.Id;
            await _unitOfWork.Orders.Insert(order);
            orderItems = orderItems.Select(m =>
            {
                m.OrderId = order.Id;
                return m;
            }).ToList();
            foreach (var orderItem in orderItems)
            {
                await _unitOfWork.OrderItems.Insert(orderItem);
            }
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