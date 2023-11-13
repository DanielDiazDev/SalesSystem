using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;

namespace SalesSystem.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRoles()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
