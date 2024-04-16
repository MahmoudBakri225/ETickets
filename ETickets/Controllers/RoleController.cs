using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManger;
        public RoleController(RoleManager<IdentityRole> roleManger)

        {
            this.roleManger = roleManger;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(userRoleVM.Name);
                var result = await roleManger.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Movie");
                }
                else

                {
                    ModelState.AddModelError(string.Empty, "Invalid role");
                }
            }
            return View(userRoleVM);
        }
    }
}
