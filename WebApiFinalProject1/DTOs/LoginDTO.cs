namespace WebApiFinalProject1.DTOs
{
    public class LoginDTO
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Role { get; set; }
    }
}
