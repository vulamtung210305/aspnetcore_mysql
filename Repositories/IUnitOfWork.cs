namespace MvcMovie.Repositories;
public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IProductRepository Products { get; }
    ICustomerRepository Customers { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }

    Task CompleteAsync();
}