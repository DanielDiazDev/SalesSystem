using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using System.Linq.Expressions;

namespace SalesSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User entity)
        {
            try
            {
                _context.Set<User>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(User entity)
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

        public async Task<bool> Edit(User entity)
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

        public async Task<IQueryable<User>> Find(Expression<Func<User, bool>> filter = null)
        {
           return filter == null ? _context.Users : _context.Users.Where(filter);
        }

        public async Task<User> Get(Expression<Func<User, bool>> filter = null)
        {
            try
            {
                return await _context.Users.Where(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
