using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;
namespace WebApiFinalProject1.Service
{
    public class UserService
    {
        private readonly IBloggingAPI<User> _userRepo;
        public UserService(IBloggingAPI<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepo.AddAsync(user);
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            return await _userRepo.UpdateAsync(id, user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepo.DeleteAsync(id);
        }
    }
}
