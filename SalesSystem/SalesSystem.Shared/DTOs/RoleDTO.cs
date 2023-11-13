namespace SalesSystem.Shared.DTOs
{
    public class RoleDTO
    {
        public int RoleId { get; set; }
        public string? Description { get; set; }

        public override bool Equals(object o)
        {
            var other = o as RoleDTO;
            return other?.RoleId == RoleId;
        }
        public override int GetHashCode() => RoleId.GetHashCode();
        public override string ToString()
        {
            return Description;
        }
    }
}
