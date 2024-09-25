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
        public IActionResult AddPost(ForumPostDTO forumPostDTO)
        {

            if (!ModelState.IsValid)
            {
                return View(forumPostDTO);
            }
            var forumPost = new ForumPost();
            forumPost.Title = forumPostDTO.Title;
            forumPost.Content = forumPostDTO.Content;
            forumPost.OwnerId = forumPostDTO.OwnerId;
            forumPost.Owner = _db.Users.FirstOrDefault(t => t.Id.Equals(forumPostDTO.OwnerId));
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
        [Authorize]
        [HttpPost]
        public IActionResult AddComment(string content, string ownerId, string postId)
        {
            var comment = new Comment();
            comment.Content = content;
            comment.OwnerId = ownerId;
            comment.PostId = postId;
            comment.ParentId = postId;
            comment.Owner = _db.Users.FirstOrDefault(t => t.Id == ownerId);

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Comments", new {uid = postId});
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddReply(string content, string ownerId, string postId, string parentId)
        {
            var reply = new Comment();
            reply.Content = content;
            reply.OwnerId = ownerId;
            reply.PostId = postId;
            reply.ParentId = parentId;
            reply.Owner = _db.Users.FirstOrDefault(t => t.Id == ownerId);

            _db.Comments.Add(reply);
            _db.SaveChanges();

            return RedirectToAction("Comments", new { uid = postId });
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
