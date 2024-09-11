using ForumProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectForum.Models;
using System.Data;

namespace ForumProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public UserController(ILogger<PostController> logger, UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_userManager.Users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrantAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string uid)
        {
            var user = _db.Users.FirstOrDefault(t => t.Id == uid);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Users));
        }
    }
}
