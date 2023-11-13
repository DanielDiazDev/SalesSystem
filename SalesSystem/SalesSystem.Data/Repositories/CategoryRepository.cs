using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;


namespace SalesSystem.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
