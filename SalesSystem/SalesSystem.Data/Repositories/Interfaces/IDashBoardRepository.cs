namespace SalesSystem.Data.Repositories.Interfaces
{
    public interface IDashBoardRepository
    {
        Task<int> TotalSalesLastWeek();
        Task<string> TotalIncomesLastWeek();
        Task<int> TotalProduct();
        Task<Dictionary<string, int>> SalesLastWeek();
    }
}
