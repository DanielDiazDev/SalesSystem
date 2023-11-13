namespace SalesSystem.Shared.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public string? roleDescription { get; set; }
        public string? Password { get; set; }
    }
}
