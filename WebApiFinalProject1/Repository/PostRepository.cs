using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Repository
{
    public class PostRepository : IBloggingAPI<Post>, IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(c=>c.Comments).ToListAsync();
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(i=>i.PostId==id);
        }
        public async Task<Post> AddAsync(Post entity)
        {
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Post?> UpdateAsync(int id, Post entity)
        {
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return false;
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetPostsWithUserProfileAsync()
        {
            var query = from p in _context.Posts
                        join u in _context.Users on p.UserId equals u.UserId
                        join pr in _context.Profiles on u.UserId equals pr.UserId
                        select new
                        {
                            PostId = p.PostId,
                            p.Title,
                            p.Content,
                            UserName = u.Username,
                            u.Email,
                            Bio = pr.Bio,
                            Location = pr.Location
                        };

            return await query.ToListAsync();
        }
    }
}
