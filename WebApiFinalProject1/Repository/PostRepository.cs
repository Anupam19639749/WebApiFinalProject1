using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Repository
{
    public class PostRepository : IBloggingAPI<Post>
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
            //var existingPost = await _context.Posts.FindAsync(id);
            //if (existingPost == null) return null;

            //existingPost.Title = entity.Title;
            //existingPost.Content = entity.Content;
            //existingPost.CreatedAt = entity.CreatedAt;
            //existingPost.PlatformPosted = entity.PlatformPosted;
            //existingPost.Category = entity.Category;
            //await _context.SaveChangesAsync();
            //return existingPost;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return false;
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
