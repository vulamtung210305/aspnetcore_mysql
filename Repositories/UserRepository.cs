using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DataContext context, ILogger logger) : base(context, logger) { }
}