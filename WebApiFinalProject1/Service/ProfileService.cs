using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Service
{
    public class ProfileService
    {
        private readonly IBloggingAPI<Profile> _profileRepo;
        public ProfileService(IBloggingAPI<Profile> profileRepo)
        {
            _profileRepo = profileRepo;
        }
        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await _profileRepo.GetAllAsync();
        }
        public async Task<Profile?> GetProfileByIdAsync(int id)
        {
            return await _profileRepo.GetByIdAsync(id);
        }
        public async Task<Profile> AddProfileAsync(Profile profile)
        {
            return await _profileRepo.AddAsync(profile);
        }
        public async Task<Profile?> UpdateProfileAsync(int id, Profile profile)
        {
            return await _profileRepo.UpdateAsync(id, profile);
        }
        public async Task<bool> DeleteProfileAsync(int id)
        {
            return await _profileRepo.DeleteAsync(id);
        }
    }
}
