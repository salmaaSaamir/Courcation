using iti.ViewModelClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iti.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleMang;
        public RolesController(RoleManager<IdentityRole> _roleMang) 
        { 
            roleMang = _roleMang;
        }
        public IActionResult NewRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRole(RoleViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = newRole.RoleName;
               IdentityResult result =  await roleMang.CreateAsync(role);
                if (result.Succeeded) 
                {
                    return View(new RoleViewModel());
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }
            }
           
            return View(newRole);
        }
    }
}
