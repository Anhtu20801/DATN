using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using QLSV.Web.Models;
using QLSV.Model.Models;
using QLSV.Common;
using Microsoft.AspNetCore.Authorization;

namespace QLSV.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(RolesHelper.Role_Admin)) 
                    return RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "Admin" });
                
                if (User.IsInRole(RolesHelper.Role_Teacher))
                    return RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "Teacher" });

                return RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "Student" });

            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (await _userManager.IsInRoleAsync(user, RolesHelper.Role_Admin))
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Home", new { area = "Admin" });
                    }
                    else if (await _userManager.IsInRoleAsync(user, RolesHelper.Role_Teacher))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Instructor" });
                    }
                    else
                        return RedirectToAction("Index", "Home", new { area = "Student" });
                }
                else
                {
                    ViewBag.Message = "Đăng nhập không thành công";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }
    }
}
