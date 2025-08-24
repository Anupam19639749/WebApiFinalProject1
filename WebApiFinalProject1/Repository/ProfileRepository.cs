using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Data;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;
namespace WebApiFinalProject1.Repository
{
    public class ProfileRepository : IBloggingAPI<Profile>
    {
        private readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context.Profiles.Include(u=>u.User).ToListAsync();
        }
        public async Task<Profile?> GetByIdAsync(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }
        public async Task<Profile> AddAsync(Profile entity)
        {
            _context.Profiles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Profile?> UpdateAsync(int id, Profile entity)
        {
            _context.Profiles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null) return false;
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
