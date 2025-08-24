using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Service
{
    public class PostService
    {
        private readonly IBloggingAPI<Post> _postRepo;
        public PostService(IBloggingAPI<Post> postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepo.GetAllAsync();
        }
        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _postRepo.GetByIdAsync(id);
        }
        public async Task<Post> AddPostAsync(Post post)
        {
            return await _postRepo.AddAsync(post);
        }
        public async Task<Post?> UpdatePostAsync(int id, Post post)
        {
            return await _postRepo.UpdateAsync(id, post);
        }
        public async Task<bool> DeletePostAsync(int id)
        {
            return await _postRepo.DeleteAsync(id);
        }
    }
}
