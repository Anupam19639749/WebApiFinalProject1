namespace WebApiFinalProject1.DTOs
{
    public class UserCreateDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
    }
}
