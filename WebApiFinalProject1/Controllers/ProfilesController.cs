using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFinalProject1.DTOs;
using WebApiFinalProject1.Models;
using WebApiFinalProject1.Service;

namespace WebApiFinalProject1.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfileService _profileService;
        public ProfilesController(ProfileService profileService)
        {
            _profileService = profileService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            return Ok(profiles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _profileService.GetProfileByIdAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile(Profile profile)
        {
            var newProfile = await _profileService.AddProfileAsync(profile);
            return CreatedAtAction(nameof(GetProfileById), new { id = newProfile.ProfileId }, newProfile);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, Profile profile)
        {
            var updatedProfile = await _profileService.UpdateProfileAsync(id, profile);
            if (updatedProfile == null)
            {
                return NotFound();
            }
            return Ok(updatedProfile);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _profileService.DeleteProfileAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
