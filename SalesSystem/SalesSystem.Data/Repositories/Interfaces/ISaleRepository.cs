using SalesSystem.Model;

namespace SalesSystem.Data.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale> Register(Sale entity);
        Task<List<Sale>> Historial(string findBy, string saleNumber, string startDate, string endDate);
        Task<List<SaleDetail>> Report(string startDate, string endDate);
    }
}
