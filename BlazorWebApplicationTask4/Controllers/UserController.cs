using BlazorWebApplicationTask4.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApplicationTask4.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager) {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("[action]")]
        public void DeleteUser() {

            //    .Where(h => h.Key.Equals("UserId", StringComparison.OrdinalIgnoreCase))
            //    .FirstOrDefault();
            //var user = _userManager.Users.Where(u => u.Id.Equals(userId.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            //_userManager.sign
            //_signInManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //var userId = HttpContext.Request.Headers;
            //var user = _userManager.Users
            //    .Where(u => u.Id.Equals(userId.ToString(), StringComparison.OrdinalIgnoreCase))
            //    .FirstOrDefault();
            //_roleManager.CreateAsync(new IdentityRole("view"));
            //_userManager.AddToRoleAsync(user, "view");

            string theRole = "view";
            CreateRoleIfNeeded(theRole);
            string userId = "";
            HttpContext.Request.Headers.TryGetValue("UserId", out userId);
            var user = await _userManager.FindByIdAsync(HttpContext.Request.Headers.TryGetValue("UserId", out));
            if (user is null) {
                return;
            }
            AddRoleToUser(user, theRole);
            HttpContext.SignOutAsync();
        }
        private void AddRoleToUser(ApplicationUser user, string role) {
            var task = _userManager.IsInRoleAsync(user, role);
            if (task.IsCompletedSuccessfully) {
                return;
            }
            _userManager.AddToRoleAsync(user, "role");
        }

        /// <summary>
        /// no need in await @ present time
        /// </summary>
        /// <param name="role"></param>
        private void CreateRoleIfNeeded(string role) {
            //var roleExists =  _roleManager.Roles.Any(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase));
            //if (roleExists) {
            //    return;
            //}
            try {
            _roleManager.CreateAsync(new IdentityRole(role));
            }
            catch (Exception) {

                throw;
            }
        }
        private ApplicationUser GetUser() {
            var userId = HttpContext.Request.Headers;
            return _userManager.FindByIdAsync(userId.ToString());
        }
    }
}
