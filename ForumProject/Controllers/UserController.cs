using ForumProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectForum.Models;

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
            var deletedUser = _db.Users.FirstOrDefault(t => t.DisplayName.Equals("DELETED_USER"));
            if (deletedUser == null)
            {
                deletedUser = new SiteUser() { DisplayName = "DELETED_USER" };
            }
            if (user != null)
            {
                var userPosts = user.Posts.ToList();
                if(userPosts.Count > 0) 
                { 
                    foreach(var post in userPosts)
                    {
                        post.Owner = deletedUser;
                    }
                }
                _db.Users.Remove(user);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Users));
        }

        //public async Task<IActionResult> DelegateAdmin()
        //{
        //    var principal = this.User;
        //    var user = await _userManager.GetUserAsync(principal);
        //    var role = new IdentityRole()
        //    {
        //        Name = "Admin"
        //    };

        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await _roleManager.CreateAsync(role);
        //    }
        //    await _userManager.AddToRoleAsync(user, "Admin");

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
