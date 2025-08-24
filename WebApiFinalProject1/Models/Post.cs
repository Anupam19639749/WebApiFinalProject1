namespace WebApiFinalProject1.Models
{
    public class Post
    {
        public int PostId { get; set; }
        // Foreign Key
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string PlatformPosted { get; set; } = null!;
        public string Category { get; set; } = null!;
        
        // Navigation
        public User? User { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
