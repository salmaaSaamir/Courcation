using iti.Models;
using iti.ViewModelClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iti.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signinManager;
        public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signinmnger)
        {
            userManager = _userManager;
            signinManager = _signinmnger;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newRUVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newRUVM.Name;
                userModel.PasswordHash = newRUVM.Password;
                userModel.UserId = newRUVM.Code;
                IdentityResult result = await userManager.CreateAsync(userModel,newRUVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel,"student");
                    await signinManager.SignInAsync(userModel, false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors )
                    {
                        ModelState.AddModelError("" , item.Description);
                    }
                }
            }
            return View(newRUVM);
        }
        [Authorize(Roles ="admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newRUVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newRUVM.Name;
                userModel.PasswordHash = newRUVM.Password;
                userModel.UserId = newRUVM.Code;
                IdentityResult result = await userManager.CreateAsync(userModel, newRUVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "admin");
                    await signinManager.SignInAsync(userModel, false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(newRUVM);
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel UserVm)
        {
            if (ModelState.IsValid)
            {
                //check
                ApplicationUser userModel = await userManager.FindByNameAsync(UserVm.Name);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, UserVm.Password);
                    if (found)
                    {
                        List<Claim> claim = new List<Claim>
                        {
                            new Claim("UserId", userModel.UserId.ToString() ?? string.Empty)
                        };
                        await signinManager.SignInWithClaimsAsync(userModel,UserVm.remmeberMe,claim);
                          
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Username and password invalid");
            }
            return View(UserVm);
        }
        public async Task<IActionResult> LogOut()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
