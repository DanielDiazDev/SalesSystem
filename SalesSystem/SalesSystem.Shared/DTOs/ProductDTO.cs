namespace SalesSystem.Shared.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        //public string? CategoryDescripction { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
