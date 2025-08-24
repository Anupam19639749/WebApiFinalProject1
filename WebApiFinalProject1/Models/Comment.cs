namespace WebApiFinalProject1.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

        public string Content { get; set; } = null!;
        public bool IsApproved { get; set; }

        // Navigation
        public Post? Post { get; set; }
        public User? User { get; set; }
    }
}
