using ETickets.IRepository;
using ETickets.Models;
using ETickets.Repository;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ETickets.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileRepository _profileRepository;

        public ProfileController(UserManager<ApplicationUser> userManager, IProfileRepository profileRepository)
        {
            _userManager = userManager;
            _profileRepository = profileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _profileRepository.GetUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }
            var model = new ApplicationUserVM
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _profileRepository.GetUserAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.Name;
                user.Email = model.Email;

                var result = await _profileRepository.UpdateUserAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Movie");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);

        }

     
            
    }
}
