using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

        public TokenController(IToken tokenService, IConfiguration _config)
        {
            _tokenService = tokenService;
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        }
    }
}
