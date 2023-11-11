using QLNS1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<User> userManager;

        public AdminController(UserManager<User> usrMgr)
        {
            userManager = usrMgr;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NormalUser user)
        {
            if (ModelState.IsValid)
            {
                User appUser = new User
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}