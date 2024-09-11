using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ForumProject.Controllers
{
    public class HomeController : Controller
    {
 
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var posts = _db.Posts;
            return View(posts);
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