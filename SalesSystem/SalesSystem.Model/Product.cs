namespace SalesSystem.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set;}
        public virtual ICollection<SaleDetail> SaleDetails { get; } = new List<SaleDetail>();

        public virtual Category? CategoryNavigationId { get; set; }

    }

}