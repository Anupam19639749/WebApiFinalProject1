namespace WebApiFinalProject1.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        // Foreign Key
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; } = null!;
        public string Location { get; set; } = null!;

        // Navigation
        public User? User { get; set; }
    }
}
