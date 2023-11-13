using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using System.Globalization;

namespace SalesSystem.Data.Repositories
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashBoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> SalesLastWeek()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            try
            {
                IQueryable<Sale> saleQuery = _context.Sales.AsQueryable();
                if(saleQuery.Count() > 0)
                {
                    DateTime? lastDate = _context.Sales.OrderByDescending(s => s.CreatedDate).Select(s => s.CreatedDate).First();
                    lastDate = lastDate.Value.AddDays(-7);

                    IQueryable<Sale> query = _context.Sales.Where(s => s.CreatedDate.Value.Date >= lastDate.Value.Date);

                    result = query
                        .GroupBy(s => s.CreatedDate.Value.Date).OrderBy(g => g.Key)
                        .Select(ds => new { date = ds.Key.ToString("dd/MM/yyyy"), total = ds.Count()})
                        .ToDictionary(r => r.date, r => r.total);
                }
                return result;
            }
            catch 
            {
                throw;
            }

        }

        public async Task<string> TotalIncomesLastWeek()
        {
            decimal result = 0;
            try
            {
                IQueryable<Sale> saleQuery = _context.Sales.AsQueryable();
                if (saleQuery.Count() > 0)
                {
                    DateTime? lastDate = _context.Sales.OrderByDescending(s => s.CreatedDate).Select(s => s.CreatedDate).First();
                    lastDate = lastDate.Value.AddDays(-7);

                    IQueryable<Sale> query = _context.Sales.Where(s => s.CreatedDate.Value.Date >= lastDate.Value.Date);
                    result = query
                        .Select(s => s.Total)
                        .Sum(s => s.Value);
                }
                return Convert.ToString(result, new CultureInfo("es-AR"));
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalProduct()
        {
            try
            {
                IQueryable<Product> query = _context.Products;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalSalesLastWeek()
        {
            int total = 0;
            try
            {
                IQueryable<Sale> saleQuery = _context.Sales.AsQueryable();

                if (saleQuery.Count() > 0)
                {
                    DateTime? lastDate = _context.Sales.OrderByDescending(s=>s.CreatedDate).Select(s => s.CreatedDate).First();

                    lastDate = lastDate.Value.AddDays(-7);

                    IQueryable<Sale> query = _context.Sales.Where(v => v.CreatedDate.Value.Date >= lastDate.Value.Date);
                    total = query.Count();
                }

                return total;
            }
            catch
            {
                throw;
            }
        }
    }
}
