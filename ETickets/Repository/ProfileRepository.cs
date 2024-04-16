using ETickets.IRepository;
using ETickets.Models;
using Microsoft.AspNetCore.Identity;

namespace ETickets.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
