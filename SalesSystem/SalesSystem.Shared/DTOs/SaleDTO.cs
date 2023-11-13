namespace SalesSystem.Shared.DTOs
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public string? DocumentNumber { get; set; }
        public string? PaymentType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? Total { get; set; }
        public string? TotalText
        {

            get
            {
                decimal? sum = 0;
                if (SaleDetails.Count > 0)
                    sum = SaleDetails.Sum(p => p.Total);

                return sum.ToString();
            }
        }
        public virtual List<SaleDetailDTO>? SaleDetails { get; set; }
    }
}
