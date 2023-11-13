namespace SalesSystem.Model
{
    public class Role
    {
        public int RoleId { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<User> Users { get; } = new List<User>();
    }

}