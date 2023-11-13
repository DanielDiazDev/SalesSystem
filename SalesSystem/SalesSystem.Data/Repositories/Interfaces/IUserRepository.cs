using SalesSystem.Model;
using System.Linq.Expressions;

namespace SalesSystem.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> Get(Expression<Func<User, bool>> filter = null);
        Task<User> Create(User entity);
        Task<bool> Edit(User entity);
        Task<bool> Delete(User entity);
        Task<IQueryable<User>> Find(Expression<Func<User, bool>> filter = null);
    }
}
