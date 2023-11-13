namespace SalesSystem.Shared.DTOs
{
    public class SaleDetailDTO
    {
        public int ProductId { get; set; }
        public string? ProductDescripction { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }
    }
}
