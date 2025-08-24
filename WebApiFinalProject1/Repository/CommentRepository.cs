using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Repository
{
    public class CommentRepository : IBloggingAPI<Comment>
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

    }
}
