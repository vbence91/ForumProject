using ForumProject.Data;
using ForumProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public IActionResult Comments(string uid)
        {
            var post = _db.Posts.FirstOrDefault(t =>t.Uid == uid);
            
            return View(post);
        }
        [HttpPost]
        public IActionResult AddComment(string content, string ownerId, string postId)
        {
            var comment = new ForumPostComment();
            comment.Content = content;
            comment.OwnerId = ownerId;
            comment.PostId = postId;
            comment.Owner = _db.Users.FirstOrDefault(t => t.Id == ownerId);

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUserPost(int uid)
        {
            var post = _db.Posts.FirstOrDefault(t => t.Uid.Equals(uid));

            if (post != null)
            {
                _db.Posts.Remove(post);
            }
            _db.SaveChanges();
            return RedirectToAction("User", "User", new { area = "" });
        }
    }
}
