using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
            => (_userManager, _signInManager) = (userManager, signInManager);

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var userIdentity = await _userManager.FindByNameAsync(model.UserName);
            var isValidUser = await _userManager.CheckPasswordAsync(userIdentity, model.Password);

            if (isValidUser)
            {
                var task = await _signInManager.PasswordSignInAsync(userIdentity, model.Password, false, false);
                if (task.Succeeded)
                    return Redirect(model.ReturnUrl);
            }

            return View();
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register(string returnUrl) => View(new RegisterViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var identityUser = new IdentityUser(model.UserName);
            await _userManager.CreateAsync(identityUser, model.Password);

            return RedirectToActionPermanent("Login", new { returnUrl = model.ReturnUrl });
        }
    }
}
