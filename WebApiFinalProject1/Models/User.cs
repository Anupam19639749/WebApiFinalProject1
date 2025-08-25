namespace WebApiFinalProject1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }

        // Foreign Key
        public string? Role { get; set; }

        // Navigation
        public Profile? Profile { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
