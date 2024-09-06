using ForumProject.Data;
using ForumProject.Models;
using ForumProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectForum.Models;
using System.Diagnostics;

namespace ForumProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
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


        public IActionResult Index()
        {
            var posts = _db.Posts;
            return View(posts);
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
            if(user != null)
            {
                _db.Users.Remove(user);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Users));
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPost(ForumPostViewModel forumPostVM)
        {
            
            if(!ModelState.IsValid)
            {
                return View(forumPostVM);
            }
            var forumPost = new ForumPost();
            forumPost.Title = forumPostVM.Title;
            forumPost.Content = forumPostVM.Content;
            forumPost.OwnerId = forumPostVM.OwnerId;
            forumPost.Owner = _db.Users.FirstOrDefault(t => t.Id.Equals(forumPostVM.OwnerId));
            _db.Posts.Add(forumPost);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}