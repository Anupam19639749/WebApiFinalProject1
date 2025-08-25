using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Interface
{
    public interface IBloggingAPI<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }

    public interface IUserRepository
    {
        Task<object?> GetUserStatsAsync(int userId);
        Task<DateTime?> GetUserLastActivityAsync(int userId);
        Task<IEnumerable<User>> GetUsersWithNoPostsAsync();
    }

    public interface IPostRepository
    {
        Task<IEnumerable<object>> GetPostsWithUserProfileAsync();
    }

    public interface ICommentRepository
    {
        Task<IEnumerable<object>> GetCommentsForPostWithUserAsync(int postId);
        Task<IEnumerable<object>> GetCommentsByUserAsync(int userId);
    }
}
