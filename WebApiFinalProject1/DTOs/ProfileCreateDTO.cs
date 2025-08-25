namespace WebApiFinalProject1.DTOs
{
    public class ProfileCreateDTO
    {
        public int UserId { get; set; }   // Existing UserId
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
    }
}
