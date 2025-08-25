using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Repository
{
    public class UserRepository : IBloggingAPI<User>, IUserRepository
    {
        private readonly AppDbContext _context; 
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(p=> p.Posts).Include(c=>c.Comments).ToListAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(p=>p.Posts).Include(c=>c.Comments).FirstOrDefaultAsync(i => i.UserId == id);
        }

        public async Task<User> AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<User?> UpdateAsync(int id, User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .Include(u => u.Comments) // comments made by this user on others' posts
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return false;
            _context.Comments.RemoveRange(user.Comments);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ 1. User details with counts
        public async Task<object?> GetUserStatsAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    u.UserId,
                    u.Username,
                    PostCount = u.Posts.Count,
                    CommentCount = u.Comments.Count
                })
                .FirstOrDefaultAsync();
        }

        // ✅ 2. User’s last activity
        public async Task<DateTime?> GetUserLastActivityAsync(int userId)
        {
            return await _context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => (DateTime?)p.CreatedAt)
                .FirstOrDefaultAsync();
        }

        // 3. Users with no posts
        public async Task<IEnumerable<User>> GetUsersWithNoPostsAsync()
        {
            return await _context.Users
                .Include(u => u.Posts)
                .Where(u => !u.Posts.Any())
                .ToListAsync();
        }
    }
}
