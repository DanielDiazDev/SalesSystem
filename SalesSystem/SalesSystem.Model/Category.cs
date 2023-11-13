namespace SalesSystem.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

}