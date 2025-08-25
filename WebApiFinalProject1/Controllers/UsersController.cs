using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFinalProject1.DTOs;
using WebApiFinalProject1.Models;
using WebApiFinalProject1.Service;

namespace WebApiFinalProject1.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetUserStats(int id)
        {
            var stats = await _userService.GetUserStatsAsync(id);
            if (stats == null) return NotFound();
            return Ok(stats);
        }

        [HttpGet("{id}/last-activity")]
        public async Task<IActionResult> GetUserLastActivity(int id)
        {
            var lastActivity = await _userService.GetUserLastActivityAsync(id);
            if (lastActivity == null) return NotFound("No activity found");
            return Ok(new { LastActivity = lastActivity });
        }

        [HttpGet("no-posts")]
        public async Task<IActionResult> GetUsersWithNoPosts()
        {
            var users = await _userService.GetUsersWithNoPostsAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateDTO dto)
        {
            var userEntity = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = dto.Role,
                IsActive = dto.IsActive
            };
            var newUser = await _userService.AddUserAsync(userEntity);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
