using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using System.Linq.Expressions;

namespace SalesSystem.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product entity)
        {
            try
            {
                _context.Set<Product>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(Product entity)
        {
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Edit(Product entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<Product>> Find(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null ? _context.Products : _context.Products.Where(filter);
        }

        public async Task<Product> Get(Expression<Func<Product, bool>> filter = null)
        {
            try
            {
                return await _context.Products.Where(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
