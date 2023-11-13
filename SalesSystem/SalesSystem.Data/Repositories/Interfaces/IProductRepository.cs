using SalesSystem.Model;
using System.Linq.Expressions;

namespace SalesSystem.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Get(Expression<Func<Product, bool>> filter = null);
        Task<Product> Create(Product entity);
        Task<bool> Edit(Product entity);
        Task<bool> Delete(Product entity);
        Task<IQueryable<Product>> Find(Expression<Func<Product, bool>> filter = null);
    }
}
