using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using System.Globalization;

namespace SalesSystem.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> Historial(string findBy, string saleNumber, string startDate, string endDate)
        {
            IQueryable<Sale> query = _context.Sales;

            if (findBy == "date")
            {

                DateTime start_Date = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-AR"));
                DateTime end_Date = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-AR"));

                return query.Where(s =>
                    s.CreatedDate.Value.Date >= start_Date.Date &&
                    s.CreatedDate.Value.Date <= end_Date.Date
                )
                .Include(sd => sd.SaleDetails)
                .ThenInclude(p => p.ProductNavigationId)
                .ToList();

            }
            else
            {
                return query.Where(s => s.DocumentNumber == saleNumber)
                  .Include(sd => sd.SaleDetails)
                  .ThenInclude(p => p.ProductNavigationId)
                  .ToList();
            }
        }

        public async Task<Sale> Register(Sale entity)
        {
            Sale saleGenerated = new Sale();

            //usaremos transacion, ya que si ocurre un error en algun insert a una tabla, debe reestablecer todo a cero, como si no hubo o no existió ningun insert
            using (var transaction = _context.Database.BeginTransaction())
            {
                int quantityDigits = 4;
                try
                {
                    foreach (SaleDetail sd in entity.SaleDetails)
                    {
                        Product productFound = _context.Products.Where(p => p.ProductId == sd.ProductId).First();

                        productFound.Stock = productFound.Stock - sd.Quantity;
                        _context.Products.Update(productFound);
                    }
                    await _context.SaveChangesAsync();


                    DocumentNumber correlative = _context.DocumentNumbers.First();

                    correlative.LastNumber = correlative.LastNumber + 1;
                    correlative.CreatedDate = DateTime.Now;

                    _context.DocumentNumbers.Update(correlative);
                    await _context.SaveChangesAsync();


                    string zeros = string.Concat(Enumerable.Repeat("0", quantityDigits));
                    string salesNumber = zeros + correlative.LastNumber.ToString();
                    salesNumber = salesNumber.Substring(salesNumber.Length - quantityDigits, quantityDigits);

                    entity.DocumentNumber = salesNumber;

                    await _context.Sales.AddAsync(entity);
                    await _context.SaveChangesAsync();

                    saleGenerated = entity;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                return saleGenerated;
            }
        }

        public async Task<List<SaleDetail>> Report(string startDate, string endDate)
        {
            DateTime start_date = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-AR"));
            DateTime end_date = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-AR"));

            List<SaleDetail> listResume = await _context.SaleDetails
                .Include(p => p.ProductNavigationId)
                .Include(s => s.SaleNavigationId)
                .Where(dv => dv.SaleNavigationId.CreatedDate.Value.Date >= start_date.Date && dv.SaleNavigationId.CreatedDate.Value.Date <= end_date.Date)
                .ToListAsync();

            return listResume;
        }
    }
}
