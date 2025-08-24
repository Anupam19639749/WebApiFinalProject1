namespace WebApiFinalProject1.Models
{
    public class Role
    {
        public string? RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
