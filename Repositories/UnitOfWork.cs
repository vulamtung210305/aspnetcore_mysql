using MvcMovie.Data;

namespace MvcMovie.Repositories;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;
    private readonly ILogger _logger;

    public IUserRepository Users { get; private set; }
    public IProductRepository Products { get; private set; }
    public ICustomerRepository Customers { get; private set; }
    public IOrderRepository Orders { get; private set; }
    public IOrderItemRepository OrderItems { get; private set; }

    public UnitOfWork(DataContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        Users = new UserRepository(context, _logger);
        Products = new ProductRepository(context, _logger);
        Customers = new CustomerRepository(context, _logger);
        Orders = new OrderRepository(context, _logger);
        OrderItems = new OrderItemRepository(context, _logger);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}