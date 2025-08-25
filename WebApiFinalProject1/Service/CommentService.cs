using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Service
{
    public class CommentService
    {
        private readonly IBloggingAPI<Comment> _commentRepo;
        private readonly ICommentRepository _commentRepository;
        public CommentService(IBloggingAPI<Comment> commentRepo, ICommentRepository commentRepository)
        {
            _commentRepo = commentRepo;
            _commentRepository = commentRepository;
        }
        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepo.GetAllAsync();
        }
        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _commentRepo.GetByIdAsync(id);
        }
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            return await _commentRepo.AddAsync(comment);
        }
        public async Task<Comment?> UpdateCommentAsync(int id, Comment comment)
        {
            return await _commentRepo.UpdateAsync(id, comment);
        }
        public async Task<bool> DeleteCommentAsync(int id)
        {
            return await _commentRepo.DeleteAsync(id);
        }
        public async Task<IEnumerable<object>> GetCommentsForPostWithUserAsync(int postId)
        {
            return await _commentRepository.GetCommentsForPostWithUserAsync(postId);
        }
        public async Task<IEnumerable<object>> GetCommentsByUserAsync(int userId)
        {
            return await _commentRepository.GetCommentsByUserAsync(userId);
        }
    }
}
