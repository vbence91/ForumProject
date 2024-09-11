using ForumProject.Data;
using ForumProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectForum.Models;

namespace ForumProject.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _db;

        public PostController(ILogger<PostController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
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

            if (!ModelState.IsValid)
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
            return RedirectToAction("Index", "Home", new { area = ""});
        }
    }
}
