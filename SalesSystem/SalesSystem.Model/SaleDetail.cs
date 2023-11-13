namespace SalesSystem.Model
{
    public class SaleDetail
    {
        public int SaleDetailId { get; set; }

        public int? SaleId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? Total { get; set; }

        public virtual Product? ProductNavigationId { get; set; }

        public virtual Sale? SaleNavigationId { get; set; }
    }

}