namespace SalesSystem.Shared.DTOs
{
    public class DashBoardDTO
    {
        public int SalesTotal { get; set; }
        public string? IncomesTotal { get; set; }
        public int ProductsTotal { get; set; }
        public List<SaleWeeklyDTO>? SalesLastWeek { get; set; }
    }
}
