using ETickets.Models;
using Microsoft.AspNetCore.Identity;

namespace ETickets.IRepository
{
    public interface IProfileRepository
    {
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
    }
}
