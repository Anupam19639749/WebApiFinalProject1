using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Repository
{
    public class CommentRepository : IBloggingAPI<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
        public async Task<Comment> AddAsync(Comment entity)
        {
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Comment?> UpdateAsync(int id, Comment entity)
        {
            _context.Comments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var comment =  _context.Comments.Find(id);
            if (comment == null) return false;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetCommentsForPostWithUserAsync(int postId)
        {
            var query = from c in _context.Comments
                        join u in _context.Users on c.UserId equals u.UserId
                        where c.PostId == postId
                        select new
                        {
                            c.CommentId,
                            c.Content,
                            UserName = u.Username,
                            Email = u.Email,
                            UserRole = u.Role
                        };

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<object>> GetCommentsByUserAsync(int userId)
        {
            var query = from c in _context.Comments
                        join p in _context.Posts on c.PostId equals p.PostId
                        where c.UserId == userId
                        select new
                        {
                            c.CommentId,
                            Comment = c.Content,
                            PostTitle = p.Title,
                            Category = p.Category,
                            CreatedAt = p.CreatedAt,
                            PostId = p.PostId
                        };
            return await query.ToListAsync();
        }

    }
}
