namespace SalesSystem.Model
{
    public class Sale
    {
        public int SaleId { get; set; }

        public string? DocumentNumber { get; set; }

        public string? PaymentType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public decimal? Total { get; set; }

        public virtual ICollection<SaleDetail> SaleDetails { get; } = new List<SaleDetail>();
    }

}