using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Repository
{
    public class UserRepository : IBloggingAPI<User>
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
    }
}
