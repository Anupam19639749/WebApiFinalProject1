using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.DTOs;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;
namespace WebApiFinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IToken _tokenService;
        private readonly AppDbContext _context;

        public TokenController(IToken tokenService, IConfiguration _config, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(LoginDTO login)
        {
            if(login != null && !string.IsNullOrEmpty(login.Username) && !string.IsNullOrEmpty(login.PasswordHash))
            {
                var user = await GetUser(login.Username, login.PasswordHash, login.Role);
                if (user != null && user.UserId != 0)
                {
                    var token = _tokenService.GenerateToken(user);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                } 
            }
            else
            {
                return BadRequest("Invalid client request");
            }
        }

        private async Task<User?> GetUser(string username, string password, string role)
        { 
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password && u.Role == role);
        }

    }
}
