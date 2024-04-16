using ETickets.Models;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;

        SignInManager<ApplicationUser> signIn;

        public AccountController(UserManager<ApplicationUser> userManager,

              SignInManager<ApplicationUser> signIn)
        {
            this.userManager = userManager;

            this.signIn = signIn;
        }


        [HttpGet]

        public IActionResult Regestration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Regestration(ApplicationUserVM userVM)

        {

            if (ModelState.IsValid)

            {

                ApplicationUser user = new ApplicationUser() { UserName = userVM.Name, Email = userVM.Email, PasswordHash = userVM.Password };
                var result = await userManager.CreateAsync(user, userVM.Password);

                if (result.Succeeded)

                {
                    await signIn.SignInAsync(user, true);
                    return RedirectToAction("Index", "Movie");
                }

                return View(userVM);
            }


            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userVM)

        {
            if (ModelState.IsValid)

            {
                var user = await userManager.FindByNameAsync(userVM.UserName);

                if (user != null)

                {
                    bool check = await userManager.CheckPasswordAsync(user, userVM.Password);
                    if (check)

                    {

                        await signIn.SignInAsync(user, userVM.RememberMe); return RedirectToAction("Index", "Movie");

                    }

                    //invalid password 
                        ModelState.AddModelError("invalidP", "invalid password");

                }

                //invalid username
                else {
                    ModelState.AddModelError("invalidu", "invalid password");
                }
                
            }
            return View(userVM);
        }
        public async Task<IActionResult> Logout()

        {
            await signIn.SignOutAsync();

            return RedirectToAction("Index", "Movie");
        }

    }
}
